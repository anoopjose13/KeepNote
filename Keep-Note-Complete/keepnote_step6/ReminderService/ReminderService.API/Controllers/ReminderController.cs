using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReminderService.API.Exceptions;
using ReminderService.API.Models;
using ReminderService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReminderService.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReminderController : Controller
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }
        /// <summary>
        /// Used to get all reminders
        /// </summary>
        /// <returns></returns>
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_reminderService.GetAllReminders());
            }
            catch (ReminderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Used to get all reminder by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_reminderService.GetReminderById(id));
            }
            catch (ReminderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Save the reminder
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns></returns>
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Reminder reminder)
        {
            try
            {
                return Created("", _reminderService.CreateReminder(reminder));
            }
            catch (ReminderNotCreatedException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Used to update the reminder
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reminder"></param>
        /// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Reminder reminder)
        {
            try
            {
                return Ok(_reminderService.UpdateReminder(id, reminder));

            }
            catch (ReminderNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Used to delete the reminder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_reminderService.DeleteReminder(id));
            }
            catch (ReminderNotFoundException ex)
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
