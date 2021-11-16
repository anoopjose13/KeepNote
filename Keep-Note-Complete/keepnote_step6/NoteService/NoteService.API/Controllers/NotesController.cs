using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteService.API.Exceptions;
using NoteService.API.Models;
using NoteService.API.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteService.API.Controllers
{
    //[EnableCors("GoogleKeep")]
   // [Authorize]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INoteService _noteService;
        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        /// <summary>
        /// Used to get the notes by userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // GET api/<controller>/5
        [HttpGet]
        public IActionResult Get(string userId)
        {
            try
            {
                return Ok(_noteService.GetAllNotes(userId));
            }
            catch (NoteNotFoundExeption ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Used to save the note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        // POST api/<controller>
        //[Route("AddNoteUser")]
        [HttpPost]
        public IActionResult Post([FromBody]NoteUser note)
        {
            try
            {
                return Created("", _noteService.CreateNote(note));
            }
            catch (NoteNotFoundExeption ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// update note by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]Note note)
        {
            try
            {
                return Ok(_noteService.AddNote(note.CreatedBy,note));
            }
            catch (NoteNotFoundExeption ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Used to delete note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string userId,int id)
        {
            try
            {
                return Ok(_noteService.DeleteNote(userId,id));
            }
            catch (NoteNotFoundExeption ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Get note by userId and noteId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        [Route("GetNoteByNoteId")]
        [HttpGet]
        public IActionResult Get(string userId,int noteId)
        {
            try
            {
                return Ok(_noteService.GetNote(userId,noteId));
            }
            catch (NoteNotFoundExeption ex)
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
