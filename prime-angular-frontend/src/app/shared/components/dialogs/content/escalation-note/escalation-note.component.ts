import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Admin } from '@auth/shared/models/admin.model';
import { BehaviorSubject } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';

export class EscalateEnrolleeAction {
  public note: string;
  public assigneeId: number;
}

@Component({
  selector: 'app-escalation-note',
  templateUrl: './escalation-note.component.html',
  styleUrls: ['./escalation-note.component.scss']
})
export class EscalationNoteComponent implements OnInit {
  @Input() public enrolleeId: number;
  public adjudicators$: BehaviorSubject<Admin[]>;
  public form: FormGroup;

  constructor(
    private adjudicationResource: AdjudicationResource,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.enrolleeId = data.data.enrolleeId;
    this.adjudicators$ = new BehaviorSubject<Admin[]>([]);
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onCancel() {
    this.dialogRef.close();
  }

  public onEscalate(assigneeId: number) {
    if (this.form.valid) {
      this.adjudicationResource.createAdjudicatorNote(this.enrolleeId, this.note.value)
        .pipe(
          exhaustMap((note: EnrolleeNote) =>
            this.adjudicationResource.createEnrolmentEscalation(this.enrolleeId, note.id, assigneeId)
          )
        ).subscribe(() => this.dialogRef.close({ reload: true }))
    }
    this.note.markAsTouched();
  }

  public ngOnInit(): void {
    this.getAdjudicators();
    this.createFormInstance();
  }

  private getAdjudicators() {
    this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => this.adjudicators$.next(adjudicators));
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: false,
        },
        [Validators.required]
      ]
    });
  }

}
