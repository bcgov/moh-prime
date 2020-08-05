import { Router, ActivatedRoute, CanDeactivate } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';

export interface IBaseEnrolmentProfilePage {
  form: FormGroup;
  enrolment: Enrolment;
  onSubmit(): void;
  canDeactivate(): Observable<boolean> | boolean;
}

export abstract class BaseEnrolmentProfilePage extends BaseEnrolmentPage implements IBaseEnrolmentProfilePage {
  public form: FormGroup;
  public enrolment: Enrolment;

  protected allowRoutingWhenDirty: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService
  ) {
    super(route, router);

    this.allowRoutingWhenDirty = false;
  }

  public onSubmit(beenThroughTheWizard: boolean = false): void {
    if (this.form.valid) {
      this.onSubmitFormIsValid();

      if (this.isInitialEnrolment) {
        // Update using the form which could contain changes
        const payload = this.enrolmentStateService.enrolment;

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
        // Allow routing to occur without invoking the deactivation,
        // modal to persist form state being dirty between views
        this.allowRoutingWhenDirty = true;
        this.nextRouteAfterSubmit();
      }
    } else {
      this.onSubmitFormIsInvalid();
      this.form.markAllAsTouched();
      this.utilService.scrollToErrorSection();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty && !this.allowRoutingWhenDirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  protected abstract createFormInstance(): void;
  protected abstract initForm(): void;

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsValid(): void {
    // Not Implemented
  }

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsInvalid(): void {
    // Not Implemented
  }

  /**
   * @description
   * Post-submission hook for execution.
   */
  protected afterSubmitIsSuccessful(): void {
    // Not Implemented
  }

  /**
   * @description
   * Redirect to the next route after a valid submission.
   *
   * @params nextRoutePath Optional next route, or defaults to overview
   */
  protected nextRouteAfterSubmit(nextRoutePath: string = EnrolmentRoutes.OVERVIEW): void {
    this.routeTo(nextRoutePath);
  }

  protected patchForm(): void {
    // Store a local copy of the enrolment for views
    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.isProfileComplete = this.enrolmentService.isProfileComplete;

    // Attempt to patch the form if not already patched
    this.enrolmentStateService.setEnrolment(this.enrolment);
  }
}
