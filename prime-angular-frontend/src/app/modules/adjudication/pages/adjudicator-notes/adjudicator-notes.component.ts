import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Subscription, BehaviorSubject, pipe } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';

@Component({
  selector: 'app-adjudicator-notes',
  templateUrl: './adjudicator-notes.component.html',
  styleUrls: ['./adjudicator-notes.component.scss']
})
export class AdjudicatorNotesComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public columns: string[];
  public adjudicatorNotes$: BehaviorSubject<DateContent[]>;
  public hasActions: boolean;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private adjudicationResource: AdjudicationResource,
    private authService: AuthService
  ) {
    this.hasActions = false;
    this.adjudicatorNotes$ = new BehaviorSubject<DateContent[]>(null);
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.adjudicationResource
        .createAdjudicatorNote(this.route.snapshot.params.id, this.note.value)
        .pipe(this.toDateContentPipe())
        .subscribe((adjudicatorNote: DateContent) => {
          const notes = [adjudicatorNote, ...this.adjudicatorNotes$.value];
          this.adjudicatorNotes$.next(notes);
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
      .pipe(this.toDateContentPipe())
      .subscribe((datedContent: DateContent[]) =>
        this.adjudicatorNotes$.next(datedContent)
      );
  }

  private toDateContentPipe() {
    return pipe(
      map((adjudicationNotes: AdjudicationNote | AdjudicationNote[]) =>
        (Array.isArray(adjudicationNotes))
          ? adjudicationNotes.map(this.toDateContent.bind(this))
          : this.toDateContent(adjudicationNotes)
      )
    );
  }

  private toDateContent(adjudicationNote: AdjudicationNote): DateContent {
    return {
      date: adjudicationNote.noteDate,
      name: adjudicationNote.adjudicator.idir,
      content: adjudicationNote.note
    };
  }
}
