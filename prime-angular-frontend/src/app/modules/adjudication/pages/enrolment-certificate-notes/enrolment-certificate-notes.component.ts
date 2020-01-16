import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource, MatDialog } from '@angular/material';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import {
  EnrolmentStatusReasonsComponent
} from '@shared/components/dialogs/content/enrolment-status-reasons/enrolment-status-reasons.component';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { NoteType } from '@adjudication/shared/enums/note-type.enum';

@Component({
  selector: 'app-enrolment-certificate-notes',
  templateUrl: './enrolment-certificate-notes.component.html',
  styleUrls: ['./enrolment-certificate-notes.component.scss']
})
export class EnrolmentCertificateNotesComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public columns: string[];
  public dataSource: MatTableDataSource<Enrolment>;
  public enrollee: Enrolment;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) {
    this.columns = ['appliedDate', 'name', 'status', 'approvedDate', 'actions'];
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public canApproveOrDeny(currentStatusCode: number) {
    return (currentStatusCode === EnrolmentStatus.SUBMITTED);
  }

  public canAllowEditing(currentStatusCode: number) {
    return (currentStatusCode !== EnrolmentStatus.ADJUDICATED_APPROVED);
  }

  public onSubmit() {
    if (this.form.valid) {
      this.busy = this.adjudicationResource
        .updateAdjudicationNote(this.enrollee.id, this.note.value, NoteType.EnrolmentCertificateNote)
        .subscribe(
          () => {
            this.toastService.openSuccessToast(`Enrolment certificate note has been saved.`);
          },
          (error: any) => {
            this.toastService.openErrorToast(`Enrolment certificate note could not be saved`);
            this.logger.error('[Adjudication] EnrolmentCertificateNote::onSubmit error has occurred: ', error);
          }
        );
    }
  }

  public viewEnrolmentHistory(enrolmentId: number) {
    this.router.navigate([enrolmentId, AdjudicationRoutes.PROFILE_HISTORY], { relativeTo: this.route.parent });
  }

  public reviewStatusReasons(enrolment: Enrolment) {
    const data: DialogOptions = {
      title: 'Review Status Reasons',
      icon: 'flag',
      actionText: 'Close',
      data: { enrolment },
      component: EnrolmentStatusReasonsComponent,
      cancelHide: true
    };
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe();
  }

  public approveEnrolment(id: number) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.updateEnrolmentStatus(id, EnrolmentStatus.ADJUDICATED_APPROVED)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(id))
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been approved');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be approved');
          this.logger.error('[Adjudication] Enrolments::approveEnrolment error has occurred: ', error);
        }
      );
  }

  public declineEnrolment(id: number) {
    const data: DialogOptions = {
      title: 'Decline Enrolment',
      message: 'Are you sure you want to decline this enrolment?',
      actionType: 'warn',
      actionText: 'Decline Enrolment'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.updateEnrolmentStatus(id, EnrolmentStatus.DECLINED)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(id)),
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been declined');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be declined');
          this.logger.error('[Adjudication] Enrolments::declineEnrolment error has occurred: ', error);
        }
      );
  }

  public markAsInProgress(id: number) {
    const data: DialogOptions = {
      title: 'Enable Editing',
      message: 'When enabled the enrollee will be able to update their enrolment. Are you sure you want to enable editing?',
      actionType: 'warn',
      actionText: 'Enable Editing'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.updateEnrolmentStatus(id, EnrolmentStatus.IN_PROGRESS)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(id))
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment status was reverted to In-Progress');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be reverted to In-Progress');
          this.logger.error('[Adjudication] Enrolments::markAsInProgress error has occurred: ', error);
        }
      );
  }

  public deleteEnrolment(id: number) {
    const data: DialogOptions = {
      title: 'Delete Enrolment',
      message: 'Are you sure you want to delete this enrolment?',
      actionType: 'warn',
      actionText: 'Delete Enrolment'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.deleteEnrolment(id)
            : EMPTY
        )
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been deleted');
          this.removeEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be deleted');
          this.logger.error('[Adjudication] Enrolments::deleteEnrolments error has occurred: ', error);
        }
      );
  }

  public ngOnInit() {
    this.createFormInstance();
    this.getEnrollee(this.route.snapshot.params.id);
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: false
        },
        []
      ]
    });
  }

  private getEnrollee(enrolleeId: number, statusCode?: number) {
    this.busy = this.adjudicationResource.enrollee(enrolleeId, statusCode)
      .subscribe(
        (enrolment: Enrolment) => {
          this.logger.info('ENROLMENT', enrolment);
          this.dataSource = new MatTableDataSource<Enrolment>([enrolment]);
          this.enrollee = enrolment;
          if (enrolment.enrolmentCertificateNote) {
            this.note.patchValue(this.enrollee.enrolmentCertificateNote.note);
          }
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrollee could not be retrieved');
          this.logger.error('[Adjudication] UserAgreementNotes::getEnrollee error has occurred: ', error);
          this.router.navigate([AdjudicationRoutes.ENROLMENTS]);
        }
      );
  }

  private updateEnrolment(enrolment: Enrolment) {
    this.dataSource.data = this.dataSource.data
      .map((currentEnrolment: Enrolment) =>
        (currentEnrolment.id === enrolment.id) ? enrolment : currentEnrolment
      );
  }

  private removeEnrolment(enrolment: Enrolment) {
    this.dataSource.data = this.dataSource.data
      .filter((currentEnrolment: Enrolment) => currentEnrolment.id !== enrolment.id);
  }
}
