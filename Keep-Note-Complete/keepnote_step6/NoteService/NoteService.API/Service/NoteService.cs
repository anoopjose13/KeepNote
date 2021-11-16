using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteService.API.Exceptions;
using NoteService.API.Models;
using NoteService.API.Repository;

namespace NoteService.API.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        /// <summary>
        /// Used to add new note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public NoteUser AddNote(string userId, Note note)
        {
            try
            {
                NoteUser _noteUser = new NoteUser();
                var allNote = GetAllNotes(userId).Find(n=>n.Id == note.Id);
                var result = _noteRepository.UpdateNote(allNote.Id,userId,note);
                if (result == null)
                {
                    throw new NoteNotFoundExeption("This note already exists");
                }
                else
                {
                    var retNoteUser = _noteRepository.FindByUserId(userId).Find(x => x.Id == note.Id);
                    _noteUser.Notes = new List<Note>();
                    _noteUser.Notes.Add(retNoteUser);
                    return _noteUser;
                }

            }

            catch (Exception Ex)
            {
                throw Ex;
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// Used to create new note
        /// </summary>
        /// <param name="noteUser"></param>
        /// <returns></returns>
        public NoteUser CreateNote(NoteUser noteUser)
        {
            try
            {
                NoteUser _noteUser = new NoteUser();
               var noteUserExist = _noteRepository.CheckUserExist(noteUser.UserId);
                
                if (noteUserExist)
                {
                    var noteUserDetails = _noteRepository.FindByUserId(noteUser.UserId);
                    int maxId = noteUserDetails.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    var note = noteUser.Notes.FirstOrDefault();
                    note.Id = maxId + 1;
                    var retNote = _noteRepository.AddNoteForUser(noteUser.UserId, note);
                    _noteUser.Notes = new List<Note>();
                    _noteUser.Notes.Add(retNote);
                    return _noteUser;
                }
                else
                {
                    noteUser.Notes[0].Id = 1;
                    var result = _noteRepository.CreateNote(noteUser);
                    if (result == null)
                    {
                        throw new NoteNotFoundExeption("This Note already exists");
                    }
                    else
                        return result;
                }

            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to delete new note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public bool DeleteNote(string userId, int noteId)
        {
            try
            {
                if (!_noteRepository.DeleteNote(userId,noteId))
                {
                    throw new NoteNotFoundExeption("This Note does not exist");
                }
                else
                { return true; }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to get all  note
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Note> GetAllNotes(string userId)
        {
            try
            {
                var result = _noteRepository.FindByUserId(userId);
                if (result == null)
                    throw new NoteNotFoundExeption("This user id does not exist");
                else return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to get note by noteId and userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public Note GetNote(string userId, int noteId)
        {
            try
            {
                var result = _noteRepository.FindByUserId(userId).Find(n=>n.Id == noteId);
                if (result == null)
                    throw new NoteNotFoundExeption("This user id does not exist");
                else return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
           
        }
        /// <summary>
        /// Used to update Note
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public NoteUser UpdateNote(int noteId, string userId, Note note)
        {
            try
            {
                var result = _noteRepository.UpdateNote(noteId,userId,note);
                if (result == null)
                {
                    throw new NoteNotFoundExeption("This user id does not exist");
                }
                else
                    return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        
    }
}
