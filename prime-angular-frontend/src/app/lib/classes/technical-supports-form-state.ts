import { FormArray } from '@angular/forms';

import { ContactFormState } from './contact-form-state.class';


export class TechnicalSupportsFormState extends ContactFormState {

  public buildForm(disabled: boolean = false): void {
    super.buildForm();

    this.formInstance.addControl('vendors', this.fb.array([]));
  }
}
