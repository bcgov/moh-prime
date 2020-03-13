import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material';

import { Subscription, BehaviorSubject } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-adjudicator-notes',
  templateUrl: './adjudicator-notes.component.html',
  styleUrls: ['./adjudicator-notes.component.scss']
})
export class AdjudicatorNotesComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public columns: string[];
  public dataSource: MatTableDataSource<Enrolment>;
  public adjudicatorNotes: BehaviorSubject<AdjudicationNote[]>;
  public hasActions: boolean;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private adjudicationResource: AdjudicationResource,
    private authService: AuthService
  ) {
    this.hasActions = false;
    this.adjudicatorNotes = new BehaviorSubject<AdjudicationNote[]>([]);
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.busy = this.adjudicationResource
        .createAdjudicatorNote(this.route.snapshot.params.id, this.note.value)
        .subscribe((adjudicatorNote: AdjudicationNote) => {
          const notes = [adjudicatorNote, ...this.adjudicatorNotes.value];
          this.adjudicatorNotes.next(notes);
          this.note.reset();
        });
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.getAdjudicatorNotes(this.route.snapshot.params.id);
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: !this.authService.isAdmin()
        },
        []
      ]
    });
  }

  private getAdjudicatorNotes(enrolleeId: number) {
    this.busy = this.adjudicationResource.getAdjudicatorNotes(enrolleeId)
      .subscribe((adjudicatorNotes: AdjudicationNote[]) =>
        this.adjudicatorNotes.next(adjudicatorNotes)
      );
  }
}
