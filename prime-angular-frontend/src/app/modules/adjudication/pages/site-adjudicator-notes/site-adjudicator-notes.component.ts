import { NoteType } from '@adjudication/shared/enums/note-type.enum';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-site-adjudicator-notes',
  templateUrl: './site-adjudicator-notes.component.html',
  styleUrls: ['./site-adjudicator-notes.component.scss']
})
export class SiteAdjudicatorNotesComponent implements OnInit {
  public noteType: NoteType;
  constructor() {
    this.noteType = NoteType.SiteRegistrationNote;
  }

  ngOnInit(): void {
  }

}
