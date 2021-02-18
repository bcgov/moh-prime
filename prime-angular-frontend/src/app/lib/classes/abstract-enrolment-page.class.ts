import { AbstractControl, FormGroup } from '@angular/forms';
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
  public busy: Subscription;
  public abstract formState: AbstractFormState<unknown>;
  public form: FormGroup;

  protected allowRoutingWhenDirty: boolean;

  constructor(
    // protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService
  ) { }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.onSubmitFormIsValid();
      // Indicate whether the enrolment process has reached the terminal view, or
      // "Been Through The Wizard - Heidi G. 2019"
      this.busy = this.performSubmission()
        .subscribe(() => this.afterSubmitIsSuccessful());
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  // public canDeactivate(): Observable<boolean> | boolean {
  //   const data = 'unsaved';
  //   return (this.form.dirty && !this.allowRoutingWhenDirty)
  //     ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
  //     : true;
  // }

  /**
   * @description
   * Instantiate the form instance.
   */
  protected abstract createFormInstance(): void;

  /**
   * @description
   * Initialize the form instance with model data.
   */
  protected abstract patchForm(): void;

  /**
   * @description
   * Setup form listeners.
   */
  protected abstract initForm(): void;

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
