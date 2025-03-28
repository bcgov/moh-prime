import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { BehaviorSubject } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';
import { SiteRegistrationNote } from '@shared/models/site-registration-note.model';
import { Admin } from '@auth/shared/models/admin.model';
import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';
import { AdminStatusType } from '@adjudication/shared/models/admin-status.enum';

export enum EscalationType {
  ENROLLEE = 1,
  SITE_REGISTRATION,
}

@Component({
  selector: 'app-escalation-note',
  templateUrl: './escalation-note.component.html',
  styleUrls: ['./escalation-note.component.scss']
})
export class EscalationNoteComponent implements OnInit {
  public id: number;
  public type: EscalationType;
  public adjudicators$: BehaviorSubject<Admin[]>;
  public form: UntypedFormGroup;

  constructor(
    private adjudicationResource: AdjudicationResource,
    private siteResource: SiteResource,
    private fb: UntypedFormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.id = data.data.id;
    this.type = data.data.escalationType;
    this.adjudicators$ = new BehaviorSubject<Admin[]>([]);
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public onCancel() {
    this.dialogRef.close();
  }

  public onEscalate(assigneeId: number) {
    if (this.form.valid) {
      switch (this.type) {
        case EscalationType.ENROLLEE:
          this.createEnrolleeEscalation(assigneeId);
          break;
        case EscalationType.SITE_REGISTRATION:
          this.createSiteRegistrationEscalation(assigneeId);
          break;
        default:
          break;
      }
    }
    this.note.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getAdjudicators();
  }

  private createFormInstance() {
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

  private getAdjudicators() {
    this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => this.adjudicators$.next(adjudicators.filter(a => a.status !== AdminStatusType.DISABLED)));
  }

  private createSiteRegistrationEscalation(assigneeId) {
    this.siteResource.createSiteRegistrationNote(this.id, this.note.value)
      .pipe(
        exhaustMap((note: SiteRegistrationNote) =>
          this.adjudicationResource.createSiteNotification(this.id, note.id, assigneeId)
        ),
        exhaustMap(() => this.siteResource.setSiteAdjudicator(this.id, assigneeId))
      ).subscribe(() => this.dialogRef.close({ reload: true }));
  }

  private createEnrolleeEscalation(assigneeId) {
    this.adjudicationResource.createAdjudicatorNote(this.id, this.note.value)
      .pipe(
        exhaustMap((note: EnrolleeNote) =>
          this.adjudicationResource.createEnrolleeNotification(this.id, note.id, assigneeId)
        ),
        exhaustMap(() => this.adjudicationResource.setEnrolleeAdjudicator(this.id, assigneeId))
      ).subscribe(() => this.dialogRef.close({ reload: true }));
  }
}
