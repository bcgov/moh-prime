import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable, pipe, from, EMPTY, of } from 'rxjs';
import { catchError, exhaustMap, map } from 'rxjs/operators';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Address } from '@shared/models/address.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { Enrollee } from '@shared/models/enrollee.model';

export interface IBaseEnrolmentProfilePage {
  /**
   * @description
   * Instance of the form state providing access to its API.
   */
  formState: AbstractFormState<unknown>;
  /**
   * @description
   * Instance of the form loaded from the form state.
   */
  form: FormGroup;
  /**
   * @description
   * Local copy of the enrolment for use in views.
   */
  enrolment: Enrolment;
  /**
   * @description
   * Handle submission of forms.
   */
  onSubmit(): void;
  /**
   * @description
   * Handle redirection from the view when the form is
   * dirty to prevent loss of form data.
   */
  canDeactivate(): Observable<boolean> | boolean;
}

export abstract class BaseEnrolmentProfilePage extends BaseEnrolmentPage implements IBaseEnrolmentProfilePage {
  // TODO added temporarily to allow gradual refactoring with formState, and
  // will be removed forcing responsibility on each page to manage formState
  /**
   * @description
   * Instance of the form state providing access to its API.
   */
  public formState: AbstractFormState<unknown>;
  /**
   * @description
   * Instance of the form loaded from the form state.
   */
  public form: FormGroup;

  public enrolment: Enrolment;

  public routeUtils: RouteUtils;

  protected allowRoutingWhenDirty: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService
  ) {
    super(route, router);

    this.allowRoutingWhenDirty = false;
    this.routeUtils = new RouteUtils(route, router, EnrolmentRoutes.MODULE_PATH);

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

  public onBack(route: string) {
    this.routeUtils.routeTo([EnrolmentRoutes.MODULE_PATH, this.isProfileComplete
      ? EnrolmentRoutes.OVERVIEW
      : route]);
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
  protected patchForm(): Observable<any> {
    // Will be null if enrolment has not been created
    const enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.isProfileComplete = this.enrolmentService.isProfileComplete;

    // Attempt to patch the form if not already patched, and ensure the
    // identity provider information is always populated from the claim
    return this.authService.getUser$()
      .pipe(
        // TODO add identity provider check to fork for BCeID
        exhaustMap((bcscUser: BcscUser) => {
          // An enrolment won't exist until after the first submission, and
          // patching the form state should occur in that initial view
          if (enrolment) {
            // Existing enrolments should have the identity provider
            // information populated directly from the claim, which
            // will be patched directly into the form state
            const { firstName, lastName, givenNames } = bcscUser;
            const verifiedAddress = bcscUser.verifiedAddress ?? new Address();
            enrolment.enrollee = { ...enrolment.enrollee, firstName, lastName, givenNames, verifiedAddress };
          }

          // Store a local copy of the enrolment for views
          this.enrolment = enrolment;

          // Store a local copy of the enrolment for views, but the
          // enrolment is also piped through to view for chaining
          return of([bcscUser, enrolment]);
        }),
        exhaustMap(([bcscUser, updatedEnrolment]: [BcscUser, Enrolment]) => {
          return from(this.enrolmentFormStateService.setForm(updatedEnrolment))
            .pipe(map(() => [bcscUser, updatedEnrolment]));
        })
      );
  }

  /**
   * @description
   * Handle a valid form submission.
   */
  protected handleSubmission(beenThroughTheWizard: boolean = false) {
    if (this.isInitialEnrolment) {
      // Update using the form which could contain changes, and ensure identity
      // provider information was not altered by repopulating in the payload
      this.busy = this.authService.getUser$()
        .pipe(
          // TODO add idenity provider check to fork for BCeID
          map(({ firstName, lastName, givenNames, verifiedAddress }: BcscUser) => {
            const enrolment = this.enrolmentFormStateService.json;
            enrolment.enrollee = { ...enrolment.enrollee, firstName, lastName, givenNames, verifiedAddress };
            return enrolment;
          }),
          exhaustMap((enrolment: Enrolment) => this.performHttpRequest(enrolment, beenThroughTheWizard))
        )
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
