import { FormArray, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable, Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { AbstractFormState } from './abstract-form-state.class';

export interface IEnrolmentPage {
  /**
   * @description
   * Instance of the form state providing access to its API.
   */
  formState: AbstractFormState<unknown>;
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

/**
 * @description
 * Class is used to provide a set of submission hooks and
 * functionality to pages used in enrolments.
 *
 * For example, outside of the boilerplate add getters for
 * quickly accessing AbstractControls in controllers to
 * reduce methods required in each controller.
 *
 * WARNING: Always use UntilDestroy in the controller to
 * unsubscribe from valueChanges on getters when the component
 * is destroyed. Not doing this will result in memory leaks, as
 * well as, create issues that are difficult to trace.
 *
 * @example
 * @UntilDestroy()
 * @Component({
 *   selector: 'app-example-page',
 *   templateUrl: './example-page.component.html',
 *   styleUrls: ['./example-page.component.scss']
 * })
 * export class ExamplePageComponent {
 *   public initForm(): void {
 *     this.formState.controlName.valueChanges
 *       .pipe(
 *         untilDestroyed(this),
 *         ...
 *       ).subscribe();
 *   }
 * }
 */
// TODO make AbstractFormState generic on AbstractEnrolmentPage
// export abstract class AbstractEnrolmentPage<T extends AbstractFormState<unknown>> implements IEnrolmentPage {
export abstract class AbstractEnrolmentPage implements IEnrolmentPage {
  /**
   * @description
   * Busy subscription for use when blocking content from
   * being interacted with in the template. For example,
   * during but not limited to HTTP requests.
   */
  public busy: Subscription;
  /**
   * @description
   * Form instance of the component.
   * @deprecated
   * Use the formState to access the form
   */
  public form: FormGroup;
  /**
   * @description
   * Form state
   */
  // TODO make AbstractFormState generic on AbstractEnrolmentPage
  // public abstract formState: T;
  public abstract formState: AbstractFormState<unknown>;
  /**
   * @description
   * Indicator applied after an initial submission of
   * the form occurs.
   */
  public hasAttemptedSubmission: boolean;
  /**
   * @description
   * Whether routing should be allowed after any form
   * control's value has been changed.
   */
  protected allowRoutingWhenDirty: boolean;
  /**
   * @description
   * Whitelisted set of control names that can be dirty, but
   * still allow routing. Allows for targeted route gating
   * on specific controls.
   *
   * @example
   * Form control checkboxes used as indicators, but are
   * not user entered data that could be lost.
   *
   * NOTE: allowRoutingWhenDirty must be falsey, as only one
   * of the routing checks can be used at a time.
   */
  protected canDeactivateWhitelist: string[];

  protected constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService
  ) { }

  /**
   * @description
   * Form submission event handler.
   */
  public onSubmit(): void {
    this.hasAttemptedSubmission = true;

    if (this.checkValidity(this.formState.form)) {
      this.onSubmitFormIsValid();
      this.busy = this.performSubmission()
        .subscribe((response?: any) => this.afterSubmitIsSuccessful(response));
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  /**
   * @description
   * Deactivation guard handler.
   */
  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.formState.form.dirty && !this.checkDeactivationIsAllowed())
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  /**
   * @description
   * Instantiate the form instance.
   */
  protected abstract createFormInstance(): void;

  /**
   * @description
   * Initialize the form instance with model data.
   *
   * Implementation Details:
   * Typically invoked before form initialization using the initForm
   * method, but also useful if invoked from within the initForm method
   * when listeners need to be setup before and after patching the form.
   *
   * @returns unknown to allow for flexibility when implemented, which
   * is can be useful as an observable when the sequence during patching
   * is asynchronous, but otherwise should be void
   */
  protected abstract patchForm(): unknown;

  /**
   * @description
   * Setup form listeners.
   */
  protected initForm(): void {
    // Optional method for setting up form listeners, but
    // when no listeners are required is NOOP
  }

  /**
   * @description
   * Check the validity of the form, as well as, perform
   * additional validation.
   */
  protected checkValidity(form: FormGroup | FormArray): boolean {
    return this.formUtilsService.checkValidity(form) && this.additionalValidityChecks(form.getRawValue());
  }

  /**
   * @description
   * Additional checks outside of the form validity that
   * should gate form submission.
   */
  protected additionalValidityChecks(formValue: unknown): boolean {
    return true;
  }

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsValid(): void {
    // Optional submission hook, otherwise NOOP
  }

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsInvalid(): void {
    // Optional submission hook, otherwise NOOP
  }

  /**
   * @description
   * Submission hook for execution.
   */
  protected abstract performSubmission(): Observable<unknown>;

  /**
   * @description
   * Post-submission hook for execution.
   */
  protected afterSubmitIsSuccessful(response?: unknown): void {
    // Optional submission hook, otherwise NOOP
  }

  /**
   * @description
   * Check that deactivation of the view is allowed in general
   * or specifically gated on a set of allowed control names.
   */
  private checkDeactivationIsAllowed(): boolean {
    if (!this.allowRoutingWhenDirty && this.canDeactivateWhitelist?.length) {
      return Object.keys(this.formState.form.controls)
        .filter(key => !this.canDeactivateWhitelist.includes(key))
        .every(key => !this.formState.form.controls[key].dirty);
    }

    return this.allowRoutingWhenDirty;
  }
}
