import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Injectable()
export class RouterService {


  constructor(private router: Router, private location: Location) { }
  routeToDashboard() {
    this.router.navigate(['dashboard']);
  }
  routeToLogin() {
    this.router.navigate(['login']);
  }
  routeToRegistration() {
    this.router.navigate(['registration']);
  }
  routeToEditNoteView(noteId) {
    this.router.navigate([
      'dashboard', {
        outlets: {
          noteEditOutlet : ['note', noteId, 'edit']
        }
      }
    ]);
  }

  routeBack() {
    // route back from edit to Read view
    this.location.back();
  }

  routeToNoteView() {
    // route to note view
    this.router.navigate(['dashboard/view/noteview']);
  }

  routeToListView() {
    // route to list view
    this.router.navigate(['dashboard/view/listview']);
  }
}
