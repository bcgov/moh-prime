import { NoteType } from '@adjudication/shared/enums/note-type.enum';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-enrollee-adjudicator-notes',
  templateUrl: './enrollee-adjudicator-notes.component.html',
  styleUrls: ['./enrollee-adjudicator-notes.component.scss']
})
export class EnrolleeAdjudicatorNotesComponent implements OnInit {
  public noteType: NoteType;

  constructor() {
    this.noteType = NoteType.EnrolleeAdjudicationNote;
  }

  ngOnInit(): void {
  }

}
