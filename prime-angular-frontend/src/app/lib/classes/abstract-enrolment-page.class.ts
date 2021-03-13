import { AbstractControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of, Subscription } from 'rxjs';

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
   * Instance of the form loaded from the form state.
   */
  form: AbstractControl;
  /**
   * @description
   * Handle submission of forms.
   */
  onSubmit(): void;
  // /**
  //  * @description
  //  * Handle redirection from the view when the form is
  //  * dirty to prevent loss of form data.
  //  */
  // canDeactivate(): Observable<boolean> | boolean;
}

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
   */
  public form: FormGroup;
  /**
   * @description
   * Form state
   */
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

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService
  ) { }

  public onSubmit(): void {
    this.hasAttemptedSubmission = true;

    if (this.formUtilsService.checkValidity(this.form) && this.additionalValidityChecks(this.form.getRawValue())) {
      this.onSubmitFormIsValid();
      // Indicate whether the enrolment process has reached the terminal view, or
      // "Been Through The Wizard - Heidi G. 2019"
      this.busy = this.performSubmission()
        .subscribe(() => this.afterSubmitIsSuccessful());
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty && !this.allowRoutingWhenDirty)
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
  protected afterSubmitIsSuccessful(): void {
    // Optional submission hook, otherwise NOOP
  }
}
