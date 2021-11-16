import { NotesService } from './../services/notes.service';
import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatTableDataSource } from '@angular/material';
import { Note } from '../note';
import { Reminder } from '../Reminder';
import { Category } from '../Category';
 import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-edit-note-view',
  templateUrl: './edit-note-view.component.html',
  styleUrls: ['./edit-note-view.component.css']
})
export class EditNoteViewComponent implements OnInit {
  note: Note;
  reminderList :  Array<Reminder>;
  reminder:Reminder;
  states: Array<string> = ['not-started', 'started', 'completed'];
  errMessage: string;
  displayedColumns: string[] = ['name', 'description', 'type','actions'];
  ReminderData =[];
  dataSource;
  constructor(private dialogRef: MatDialogRef<EditNoteViewComponent>,
              @Inject(MAT_DIALOG_DATA) private data: any,
              private notesService: NotesService) {
                //this.notesService.fetchNotesFromServer();
               }

  ngOnInit() {
    this.reminder = new Reminder();
    this.note = this.notesService.getNoteById(this.data.noteId);
    //this.note.category= new Category();
    if(this.note.reminders ==null){
      this.ReminderData = new Array<Reminder>();
    }
    else{
    this.ReminderData = this.note.reminders;
    }
    this.dataSource = new MatTableDataSource(this.ReminderData);
    this.note.reminders = new Array<Reminder>();
    this.note.reminders.push(this.reminder);
       
  }
  addReminders()
  {
    if(this.note.reminders[0].name != "" && this.note.reminders[0].description!= ""
      && this.note.reminders[0].type != ""){
    if(this.ReminderData.length==0)
    {
      id=1;
    }
    else{
    var id = Math.max.apply(Math, this.ReminderData.map(function(o) { return o.id; }));
    id++;
    }
        this.ReminderData.push({
        id:id,
        name:this.note.reminders[0].name,
        description:this.note.reminders[0].description,
        type:this.note.reminders[0].type

     });
    this.reminder.name='';
    this.reminder.description='';
    this.reminder.type='';
    this.dataSource = new MatTableDataSource(this.ReminderData);
    }
  }
  onSave() {
    this.note.reminders =this.ReminderData;
    this.notesService.editNote(this.note).subscribe(editNote => {
      this.dialogRef.close();
    },
    err => {
      this.errMessage = err.message;
    });
  }
  onRemove(reminder) { 
  this.ReminderData.forEach(item => {
      if(item.id === reminder.id){
        const index: number =  this.ReminderData.indexOf(item);
      this.ReminderData.splice(index,1);

      this.dataSource = new MatTableDataSource(this.ReminderData);
      }
    });
  
}
editRow(row){
this.reminder.name=row.name;
this.reminder.description=row.description;
this.reminder.type=row.type;
}
hideGrid(){
    if(this.ReminderData.length==0)
    return true
    else
    return false;

}

}





