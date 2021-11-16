using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    //[EnableCors("GoogleKeep")]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService service;
        private readonly ITokenGenerator tokengenerator;
        public AuthController(IAuthService _service, ITokenGenerator tokenGenerator)
        {
            this.service = _service;
            tokengenerator = tokenGenerator;
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            try
            {
               return Created("",service.RegisterUser(user));
               
            }
            catch (UserNotCreatedException ex)
            {
                return Conflict();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        /// <summary>
        /// Generate the token for login 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
              User _user = service.LoginUser(user.UserId, user.Password);          
                string value = tokengenerator.GetJWTToken(user.UserId);
                return Ok(value);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
