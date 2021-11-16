import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { DashboardComponent } from '../src/app/dashboard/dashboard.component';
import { NotesService } from '../src/app/services/notes.service';
import { AuthenticationService } from '../src/app/services/authentication.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { Note } from '../src/app/note';
import { Category } from '../src/app/Category';
import { Reminder } from '../src/app/Reminder';

var category = new Category();
category.id =1;
category.description ='desc1';
category.name ='name1';
var reminder = new Reminder();
reminder.id =1;
reminder.name ='rem1';
reminder.description = 'rem1';
reminder.type = 'type';
var reminderList = new Array<Reminder>();
reminderList[0]=this.reminder;

const testConfig = {
  notes: [{
      id: 1,
      title: 'Read Angular 5 blog',
      text: 'Shall do at 6 pm',
      state: 'not-started',
      creationDate:new Date(),
      createdBy:'anoop4',
      category:this.category,
      reminders:this.reminderList
    },
    {
      id: 2,
      title: 'Call Ravi',
      text: 'Track the new submissions',
      state: 'not-started',
      creationDate:new Date(),
      createdBy:'anoop4',
      category:this.category,
      reminders:this.reminderList
    }]
};

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let notesService: any;
  let spyFetchNotesFromServer: any;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardComponent ],
      imports: [ HttpClientModule ],
      schemas: [NO_ERRORS_SCHEMA],
      providers: [
      NotesService,
      AuthenticationService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    notesService = TestBed.get(NotesService);
    spyFetchNotesFromServer = spyOn(notesService, 'fetchNotesFromServer').and.returnValue(Observable.of(testConfig.notes));
  });

  it('should create', () => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    expect(component).toBeTruthy();
  });

  it('fetchNotesFromServer should be called whenever DashboardComponent is rendered', () => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    expect(notesService.fetchNotesFromServer).toHaveBeenCalled();
  });
});
