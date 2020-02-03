import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from './BaseEnrolmentPage';
import { ProgressStatus } from '../enums/progress-status.enum';
import { EnrolmentService } from '../services/enrolment.service';
import { EnrolmentResource } from '../services/enrolment-resource.service';
import { EnrolmentStateService } from '../services/enrolment-state.service';
import { LoggerService } from '@core/services/logger.service';

export interface IBaseEnrolmentProfilePage {
  form: FormGroup;
  onSubmit(): void;
  canDeactivate(): Observable<boolean> | boolean;
}

export abstract class BaseEnrolmentProfilePage extends BaseEnrolmentPage implements IBaseEnrolmentProfilePage {
  public form: FormGroup;
  public enrolment: Enrolment;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService
  ) {
    super(route, router);
  }

  public onSubmit(beenThroughTheWizard: boolean = false) {
    if (this.form.valid) {
      this.onSubmitFormIsValid();

      if (this.isInitialEnrolment) {
        const payload = this.enrolmentStateService.enrolment;

        this.logger.info('UPDATING_ENROLMENT', payload);

        // Indicate whether the enrolment process has reached the terminal view, or
        // "Been Through The Wizard - Heidi G. 2019"
        this.busy = this.enrolmentResource.updateEnrollee(payload, beenThroughTheWizard)
          .subscribe(
            () => {
              this.afterSubmitIsSuccessful();

              this.toastService.openSuccessToast('Enrolment information has been saved');
              this.form.markAsPristine();

              this.nextRouteAfterSubmit();
            },
            (error: any) => {
              this.toastService.openErrorToast('Enrolment information could not be saved');
              this.logger.error('[Enrolment] Submission error has occurred: ', error);
            }
          );
      } else {
        this.logger.info('NOT_UPDATING_ENROLMENT');

        this.form.markAsPristine();
        this.nextRouteAfterSubmit();
      }
    } else {
      this.onSubmitFormIsInvalid();
      this.form.markAllAsTouched();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  protected abstract createFormInstance(): void;
  protected abstract initForm(): void;

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsValid() {
    // Not Implemented
    this.logger.info('ON_SUBMIT_FORM_IS_VALID');
  }

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsInvalid() {
    // Not Implemented
    this.logger.info('ON_SUBMIT_FORM_IS_INVALID');
  }

  /**
   * @description
   * Post-submission hook for execution.
   */
  protected afterSubmitIsSuccessful() {
    // Not Implemented
    this.logger.info('AFTER_SUBMIT_IS_SUCCESSFUL');
  }

  /**
   * @description
   * Redirect to the next route after a valid submission.
   *
   * @params nextRoutePath Optional next route, or defaults to overview
   */
  protected nextRouteAfterSubmit(nextRoutePath: string = EnrolmentRoutes.OVERVIEW): void {
    this.logger.info('NEXT_ENROLMENT_ROUTE', nextRoutePath);

    this.routeTo(nextRoutePath);
  }

  protected patchForm(): void {
    this.enrolment = this.enrolmentService.enrolment;
    this.enrolmentStateService.enrolment = this.enrolment;
    this.isInitialEnrolment = this.enrolment.progressStatus !== ProgressStatus.FINISHED;
    this.isProfileComplete = this.enrolment.profileCompleted;
  }
}
