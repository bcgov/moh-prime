import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material';

import { Subscription, BehaviorSubject } from 'rxjs';

import { Enrolment } from '@shared/models/enrolment.model';
import { SubmissionAction } from '@shared/enums/submission-action.enum';

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

// TODO MERGE_CONFLICT
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

  public approveEnrolment(enrolment: Enrolment) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment',
      data: { enrolment },
      component: ApproveEnrolmentComponent
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: boolean }) => {
          if (result) {
            if (result.hasOwnProperty('output') && result.output !== undefined) {
              enrolment.alwaysManual = result.output;
              return this.adjudicationResource.updateEnrolleeAlwaysManual(enrolment.id, result.output);
            }
            return this.adjudicationResource.enrollee(enrolment.id);
          }
          return EMPTY;

        }),
        exhaustMap(() =>
          this.adjudicationResource
            .submissionAction(enrolment.id, SubmissionAction.APPROVE)
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(enrolment.id))
      )
      .subscribe(
        (approvedEnrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment has been approved');
          this.updateEnrolment(approvedEnrolment);
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
            ? this.adjudicationResource.submissionAction(id, SubmissionAction.LOCK_PROFILE)
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

  public markAsActive(id: number) {
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
            ? this.adjudicationResource.submissionAction(id, SubmissionAction.ENABLE_EDITING)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.enrollee(id))
      )
      .subscribe(
        (enrolment: Enrolment) => {
          this.toastService.openSuccessToast('Enrolment status was reverted to Active');
          this.updateEnrolment(enrolment);
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment status could not be reverted to Active');
          this.logger.error('[Adjudication] Enrolments::markAsActive error has occurred: ', error);
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
    if (this.authService.isSuperAdmin()) {
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
  }

  public routeTo() {
    this.router.navigate(['../../'], { relativeTo: this.route.parent });
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
