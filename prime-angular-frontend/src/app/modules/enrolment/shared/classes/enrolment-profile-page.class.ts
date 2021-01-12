import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable, pipe } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';

export interface IBaseEnrolmentProfilePage {
  form: FormGroup;
  formState: AbstractFormState<unknown>;
  enrolment: Enrolment;
  onSubmit(): void;
  canDeactivate(): Observable<boolean> | boolean;
}

export abstract class BaseEnrolmentProfilePage extends BaseEnrolmentPage implements IBaseEnrolmentProfilePage {
  public form: FormGroup;
  // TODO added temporarily to allow gradual refactoring of with formState, and
  // will be removed forcing responsibility on each page to manage formState
  public formState: AbstractFormState<unknown>;
  public enrolment: Enrolment;

  protected allowRoutingWhenDirty: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService
  ) {
    super(route, router);

    this.allowRoutingWhenDirty = false;
  }

  public onSubmit(beenThroughTheWizard: boolean = false): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.onSubmitFormIsValid();
      this.handleSubmission(beenThroughTheWizard);
    } else {
      this.onSubmitFormIsInvalid();
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
   * Patch the form with enrollee information.
   */
  protected patchForm(): void {
    // Store a local copy of the enrolment for views
    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.isProfileComplete = this.enrolmentService.isProfileComplete;

    // Attempt to patch the form if not already patched
    this.enrolmentFormStateService.setForm(this.enrolment);
  }

  /**
   * @description
   * Handle a valid form submission.
   */
  protected handleSubmission(beenThroughTheWizard: boolean = false) {
    if (this.isInitialEnrolment) {
      // Update using the form which could contain changes
      this.busy = this.performHttpRequest(this.enrolmentFormStateService.json, beenThroughTheWizard)
        .subscribe();
    } else {
      // Allow routing to occur without invoking the deactivation,
      // modal to persist form state being dirty between views
      this.allowRoutingWhenDirty = true;
      this.nextRouteAfterSubmit();
    }
  }

  /**
   * @description
   * Perform an HTTP request to store the enrollee information. By default
   * this is an update, but can be extended to perform any request.
   */
  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    // Indicate whether the enrolment process has reached the terminal view, or
    // "Been Through The Wizard - Heidi G. 2019"
    return this.enrolmentResource.updateEnrollee(enrolment, beenThroughTheWizard)
      .pipe(this.handleResponse());
  }

  /**
   * @description
   * Generic handler for the HTTP response. By default this covers update, and can
   * also be used for create actions, or extended for any response.
   */
  protected handleResponse() {
    return pipe(
      map(() => {
        this.afterSubmitIsSuccessful();

        this.toastService.openSuccessToast('Enrolment information has been saved');
        this.form.markAsPristine();

        this.nextRouteAfterSubmit();
      }),
      catchError((error: any) => {
        this.toastService.openErrorToast('Enrolment information could not be saved');
        this.logger.error('[Enrolment] Submission error has occurred: ', error);

        throw error;
      })
    );
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
}
