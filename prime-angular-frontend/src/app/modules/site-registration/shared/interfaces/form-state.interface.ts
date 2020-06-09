import { AbstractControl } from '@angular/forms';

export abstract class AbstractFormState<T> {
  /**
   * @description
   * Convert JSON into reactive form abstract controls.
   */
  public set form(model: T) {
    this.patchForm(model);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public abstract get json(): T;

  /**
   * @description
   * List of constituent model forms, which is used at minimum to
   * drive internal form helper methods.
   */
  public get forms(): AbstractControl[] {
    return [];
  }

  /**
   * @description
   * Check that all constituent forms are valid.
   */
  public get isValid(): boolean {
    return this.forms
      .reduce((valid: boolean, form: AbstractControl) => valid && form.valid, true);
  }

  /**
   * @description
   * Check that at least one constituent form is dirty.
   */
  public get isDirty(): boolean {
    return this.forms
      .reduce((dirty: boolean, form: AbstractControl) => dirty || form.dirty, false);
  }

  /**
   * @description
   * Mark all constituent forms as pristine.
   */
  public markAsPristine(): void {
    this.forms
      .forEach((form: AbstractControl) => form.markAsPristine());
  }

  /**
   * @description
   * Patch the form with the model.
   */
  protected abstract patchForm(model: T): T;
}
