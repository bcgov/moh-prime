import { FormBuilder, FormGroup } from '@angular/forms';

import { IForm } from '@lib/interfaces/form.interface';

export abstract class AbstractFormState<T> implements IForm {
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
  public abstract patchValue(data: T): void;

  /**
   * @description
   * Build the reactive form structure.
   */
  public abstract buildForm(): void;
}
