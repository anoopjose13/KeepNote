using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
      
        /// <summary>
        /// Fetch the user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Register the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            try
            {
                return Created("", _userService.RegisterUser(user));
            }
            catch (UserNotCreatedException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]User user)
        {
            try
            {
                if (_userService.UpdateUser(id, user))
                {
                    return Ok(user);
                }
                else
                { throw new UserNotFoundException("User not Exist"); }
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Delete the user y id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                return Ok(_userService.DeleteUser(id));
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
