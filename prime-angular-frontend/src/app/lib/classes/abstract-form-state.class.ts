import { FormGroup } from '@angular/forms';

/**
 * @description
 * Class is used to encapsulate page component form functionality, and
 * adaptation between the business model and form model.
 */
export abstract class AbstractFormState<T> {
  protected formInstance: FormGroup;

  /**
   * @description
   * Get the reactive form instance.
   */
  public get form(): FormGroup | null {
    return this.formInstance ?? null;
  }

  /**
   * @description
   * Get the reactive form as JSON.
   */
  public abstract get json(): T;

  /**
   * @description
   * Patch the reactive form with data.
   */
  public abstract patchValue(data: T, options?: unknown): void;

  /**
   * @description
   * Build the reactive form structure.
   */
  public abstract buildForm(): void;
}
