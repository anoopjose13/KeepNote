import { Category } from './Category';
import { Reminder } from './Reminder';
export class Note {
  id: Number;
  title: string;
  content:string;
  creationDate:Date;
  createdBy:string;
  state: string;
  category:Category;
  reminders: Array<Reminder>;
  
  constructor() {
    this.title = '';
    this.content='';
    this.state = 'not-started';
        
  }
}
