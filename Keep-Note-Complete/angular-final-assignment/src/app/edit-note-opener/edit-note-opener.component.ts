import { Component } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { EditNoteViewComponent } from './../edit-note-view/edit-note-view.component';
import { RouterService } from '../services/router.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-note-opener',
  templateUrl: './edit-note-opener.component.html',
  styleUrls: ['./edit-note-opener.component.css']
})
export class EditNoteOpenerComponent {
  constructor(private routerService: RouterService,
              private dialog: MatDialog,
              private activatedRoute: ActivatedRoute) {
    const noteId = +this.activatedRoute.snapshot.paramMap.get('noteId');
    this.dialog.open(EditNoteViewComponent, {
      data: {
        noteId: noteId
      }
    })
    .afterClosed().subscribe(
      response => {
        this.routerService.routeBack();
      }
    );
  }

}
