using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using NoteService.API.Service;
using NoteService.API.Models;
using NoteService.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace NoteService.Test
{
    public class NoteControllerTest
    {
        [Fact]
        public void GetAllNotesShouldReturnOK()
        {
            string userId = "Mukesh";
            var mockService = new Mock<INoteService>();
            mockService.Setup(svc => svc.GetAllNotes(userId)).Returns(this.GetNotes());
            var controller = new NotesController(mockService.Object);

            var actual = controller.Get(userId);

            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<List<Note>>(actionResult.Value);
        }

        [Fact]
        public void PostShouldReturnCreated()
        {
            string userId = "Mukesh";
            var note = this.GetNote();
            var noteUser = new NoteUser {UserId="Mukesh", Notes=this.GetNotes() };
            var mockService = new Mock<INoteService>();
            mockService.Setup(svc => svc.CreateNote(noteUser)).Returns(noteUser);
            var controller = new NotesController(mockService.Object);

            var actual = controller.Post(noteUser);

            var actionResult = Assert.IsType<CreatedResult>(actual);
            Assert.IsAssignableFrom<NoteUser>(actionResult.Value);
        }

        [Fact]
        public void PutShouldReturnOk()
        {
            int noteId = 131;
            string userId = "Mukesh";

            Note note = GetNote();
            note.Id = 131;
            note.Title = "IPL 2018";
            note.Content = "Mumbai Indians vs RCB match scheduled  for 4 PM is cancelled";
            note.Category = this.GetCategory();
            note.Reminders = this.GetReminder();
            note.CreatedBy = "Mukesh";
            note.CreationDate = new DateTime();

            var noteUser = new NoteUser { UserId = "Mukesh", Notes = this.GetNotes() };
            var mockService = new Mock<INoteService>();
            mockService.Setup(svc => svc.AddNote(userId, note)).Returns(noteUser);
            var controller = new NotesController(mockService.Object);

            var actual = controller.Put(note);

            var actionResult = Assert.IsType<OkObjectResult>(actual);
            Assert.IsAssignableFrom<NoteUser>(actionResult.Value);
        }

        [Fact]
        public void DeleteShouldReturnOk()
        {
            string userId = "Mukesh";
            int noteId = 101;
            var mockService = new Mock<INoteService>();
            mockService.Setup(svc => svc.DeleteNote(userId, noteId)).Returns(true);
            var controller = new NotesController(mockService.Object);

            var actual = controller.Delete(userId, noteId);

            var actionResult = Assert.IsType<OkObjectResult>(actual);
        }

        private Category GetCategory()
        {
            Category category = new Category();
            category.Id = 201;
            category.Name = "Cricket";
            category.Description = "IPL 20-20";
            category.CreatedBy = "Mukesh";
            category.CreationDate = new DateTime();
            return category;
        }

        private List<Reminder> GetReminder()
        {
            List<Reminder> reminders = new List<Reminder>();
            Reminder reminder = new Reminder();
            reminder.Id = 301;
            reminder.Name = "Email-reminder";
            reminder.Description = "sending-mails";
            reminder.Type = "email";
            reminder.CreatedBy = "Mukesh";
            reminder.CreationDate = new DateTime();
            reminders.Add(reminder);
            return reminders;
        }

        private Note GetNote()
        {
            Note note = new Note();
            note.Id = 131;
            note.Title = "IPL 2018";
            note.Content = "Mumbai Indians vs RCB match scheduled  for 4 PM";
            note.Category = this.GetCategory();
            note.Reminders = this.GetReminder();
            note.CreatedBy = "Mukesh";
            note.CreationDate = new DateTime();

            return note;
        }
        private List<Note> GetNotes()
        {
            List<Note> notes = new List<Note>();
            notes.Add(this.GetNote());
            return notes;
        }
    }
}
