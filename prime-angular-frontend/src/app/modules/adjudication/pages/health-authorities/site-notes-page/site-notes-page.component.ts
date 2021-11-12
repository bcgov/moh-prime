import { NoteType } from '@adjudication/shared/enums/note-type.enum';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-site-notes-page',
  templateUrl: './site-notes-page.component.html',
  styleUrls: ['./site-notes-page.component.scss']
})
export class SiteNotesPageComponent implements OnInit {
  public noteType: NoteType;

  constructor() {
    this.noteType = NoteType.SiteRegistrationNote;
  }

  ngOnInit(): void {
  }

}
