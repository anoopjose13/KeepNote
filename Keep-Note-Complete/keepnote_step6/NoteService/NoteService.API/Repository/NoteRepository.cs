using System.Collections.Generic;
using MongoDB.Driver;
using NoteService.API.Models;


namespace NoteService.API.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly INoteContext _noteContext;
        public NoteRepository(INoteContext noteContext)
        {
            _noteContext = noteContext;
        }
        /// <summary>
        /// Used to create Note User
        /// </summary>
        /// <param name="noteUser"></param>
        /// <returns></returns>
        public NoteUser CreateNote(NoteUser noteUser)
        {
            _noteContext.Notes.InsertOne(noteUser);
            FilterDefinition<NoteUser> filter = Builders<NoteUser>.Filter.Eq(u => u.UserId, noteUser.UserId);
            return _noteContext.Notes.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Used to delete Note
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public bool DeleteNote(string userId, int noteId)
        {

            var filter = Builders<NoteUser>.Filter.Eq(e => e.UserId, userId) & Builders<NoteUser>.Filter.ElemMatch(e => e.Notes,
                 Builders<Note>.Filter.Eq(e => e.Id, noteId));
            var result = _noteContext.Notes.DeleteManyAsync(filter).Result;
            return result.IsAcknowledged;
        }
        /// <summary>
        /// Return Notes by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Note> FindByUserId(string userId)
        {
            var filter = Builders<NoteUser>.Filter.Eq(e => e.UserId, userId) & Builders<NoteUser>.Filter.ElemMatch(e => e.Notes,
             Builders<Note>.Filter.Eq(e => e.CreatedBy, userId));
            var result = _noteContext.Notes.Find(filter).FirstOrDefault();
            return result.Notes;
        }
        /// <summary>
        /// Update note by user id an note id 
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public NoteUser UpdateNote(int noteId, string userId, Note note)
        {
            var update = Builders<NoteUser>.Update
               .Set(x=>x.Notes[-1],note);
            var filter = Builders<NoteUser>.Filter.Eq(e => e.UserId, userId) & Builders<NoteUser>.Filter.ElemMatch(e => e.Notes,
               Builders<Note>.Filter.Eq(e => e.Id, noteId));
            var result = _noteContext.Notes.FindOneAndUpdate(filter, update);
            return result;
        }
        /// <summary>
        /// AddNoteForUser
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public Note AddNoteForUser(string userId, Note note)
        {
                       
            var noteReturn =  _noteContext.Notes.UpdateOne(
            Builders<NoteUser>.Filter.Eq(x => x.UserId, userId),
            Builders<NoteUser>.Update.Push(x => x.Notes, note));
            return FindByUserId(userId).Find(x=>x.Id==note.Id);
            
        }
        /// <summary>
        /// Used to check user exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool CheckUserExist(string userId)
        {
            var filter = Builders<NoteUser>.Filter.Eq(e => e.UserId, userId) & Builders<NoteUser>.Filter.ElemMatch(e => e.Notes,
            Builders<Note>.Filter.Eq(e => e.CreatedBy, userId));
            var result = _noteContext.Notes.Find(filter).Count();
            if (result > 0)
                return true;
            else
                return false;

        }

    }
}
