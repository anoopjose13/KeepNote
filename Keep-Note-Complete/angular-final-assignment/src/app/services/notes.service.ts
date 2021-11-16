import { Injectable } from '@angular/core';
import { Note } from '../note';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { NoteUser } from '../NoteUser';
import { Reminder } from '../Reminder';
import { Category } from '../Category';
import { HttpResponse} from "@angular/common/http";
import { Register } from '../register';


@Injectable()
export class NotesService { notes: Array<Note>;
  notesSubject: BehaviorSubject<Array<Note>>;
  token: any;
  userId:string;
  reminder:Reminder = new Reminder();
  category:Category = new Category();
  noteUser:NoteUser = new NoteUser();

  constructor(private http: HttpClient, private authService: AuthenticationService) {
    this.notes = [];
    this.notesSubject = new BehaviorSubject(this.notes);
    this.token = this.authService.getBearerToken();
    this.userId = this.authService.getUserId();
  }

  fetchNotesFromServer() {
    let userId = this.userId
    return this.http.get<Array<Note>>('http://localhost:56183/api/Notes/', {
      params: { userId: userId }
    })
    .subscribe( notes => {
      this.notes = notes;
      this.notesSubject.next(this.notes);
    },
    err => {
      return Observable.throw(err);
    });
  }

  getNotes(): BehaviorSubject<Array<Note>> {
    return this.notesSubject;
  }
  
  addNote(note: Note): Observable<HttpResponse<NoteUser>> {
    this.noteUser.userId=this.userId;
    this.noteUser.notes = new Array<Note>();
    var noteData = new Note();
    noteData.content=note.content;
    noteData.title=note.title;
    noteData.createdBy=this.userId;
    this.noteUser.notes[0] = noteData;
      return this.http.post<NoteUser>('http://localhost:56183/api/Notes/',{
       "userid": this.userId,
       "notes":this.noteUser.notes
      })
       .do (addNote => {
        this.notes.push(addNote.notes[0]);
        this.notesSubject.next(this.notes);
      })
      .catch(err => {
        return Observable.throw(err);
      });
   
  }

  editNote(note: Note): Observable<HttpResponse<NoteUser>> {
    var noteData = new Note();
    noteData.content=note.content;
    noteData.title=note.title;
    noteData.createdBy=this.userId;
    noteData.id=note.id;
    return this.http.put<NoteUser>('http://localhost:56183/api/Notes/',{
       "title": note.title,
      "content":note.content,
      "createdby":this.userId,
      "state":note.state,
      "id":note.id,
      "reminders":note.reminders

    }).do(editNote => {
      const noteValue = this.notes.find(notes => notes.id === editNote.notes[0].id);
      Object.assign(noteValue, editNote.notes[0]);
        this.notesSubject.next(this.notes);
    })
    .catch(err => {
        return Observable.throw(err);
      });
  }

  getNoteById(noteId): Note {
    const noteValue = this.notes.find(note => note.id === noteId);
    return Object.assign({}, noteValue);
  }
}
