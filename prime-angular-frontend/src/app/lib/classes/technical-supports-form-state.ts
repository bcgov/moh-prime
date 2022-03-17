import { Injectable } from '@angular/core';

import { ContactFormState } from './contact-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class TechnicalSupportsFormState extends ContactFormState {

  public buildForm(disabled: boolean = false): void {
    super.buildForm();

    this.formInstance.addControl('vendors', this.fb.array([]));
  }
}
