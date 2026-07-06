import { Component, OnInit } from '@angular/core';

import { NoteType } from '@adjudication/shared/enums/note-type.enum';

@Component({
    selector: 'app-site-adjudicator-notes',
    templateUrl: './site-adjudicator-notes.component.html',
    styleUrls: ['./site-adjudicator-notes.component.scss'],
    standalone: false
})
export class SiteAdjudicatorNotesComponent implements OnInit {
  public noteType: NoteType;

  constructor() {
    this.noteType = NoteType.SiteRegistrationNote;
  }

  public ngOnInit(): void { }
}
