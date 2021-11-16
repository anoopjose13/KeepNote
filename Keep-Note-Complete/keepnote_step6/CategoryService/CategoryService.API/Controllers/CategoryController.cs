using System;
using Microsoft.AspNetCore.Mvc;
using CategoryService.API.Service;
using CategoryService.API.Models;
using CategoryService.API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CategoryService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService _service)
        {
            this.service = _service;
        }
        /// <summary>
        /// Get category details by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        // GET: api/<controller
        [Route("GetCategoryByCategoryId")]
        [HttpGet]
        public IActionResult Get(int categoryId)
        {
            try
            {
                return Ok(service.GetCategoryById(categoryId));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Get all the category details by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [Route("GetCategoryByUserId")]
        [HttpGet]
        public IActionResult Get(string userId)
        {
            try
            {
                return Ok(service.GetAllCategoriesByUserId(userId));
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Add the category details
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            try
            {
                return Created("", service.CreateCategory(category));
            }
            catch (CategoryNotCreatedException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Used to update the category by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Category category)
        {
            try
            {
                return Ok(service.UpdateCategory(id, category));

            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Delete the category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteCategory(id));
            }
            catch (CategoryNotFoundException ex)
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
