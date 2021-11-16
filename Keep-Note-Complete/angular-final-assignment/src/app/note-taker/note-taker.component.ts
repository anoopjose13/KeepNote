import { Component, OnInit } from '@angular/core';
import { Note } from '../note';
import { NotesService } from '../services/notes.service';

@Component({
  selector: 'app-note-taker',
  templateUrl: './note-taker.component.html',
  styleUrls: ['./note-taker.component.css']
})
export class NoteTakerComponent {
  errMessage: string;
  note: Note = new Note();
  notes: Array<Note> = [];

  constructor(private notesService: NotesService) {}

  takeNotes() {
    const title = this.note.title.trim();
    const text = this.note.content.trim();
    if (title === '' || text === '') {
      // add the error message when fields are empty
      this.errMessage = 'Title and Text both are required fields';
    } else {
      // add notes to service
      this.notes.push(this.note);
      this.notesService.addNote(this.note).subscribe(
        data => {
          this.errMessage =  '';
        },
        err => {
          // remove the added note if there is any error
          this.notes.pop();
          this.errMessage = err.message;
        }
      );
    }
    this.note = new Note();
  }
}
