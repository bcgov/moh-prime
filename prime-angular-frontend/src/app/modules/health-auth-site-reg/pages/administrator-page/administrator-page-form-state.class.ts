import { FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Contact } from '@lib/models/contact.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

export class AdministratorPageFormState extends AbstractFormState<Contact[]> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): Contact[] {
    if (!this.formInstance) {
      return;
    }

    // return this.formUtilsService.toPersonJson(this.formInstance.getRawValue());
  }

  public patchValue(model: Contact[]): void {
    if (!model) {
      return;
    }

    // this.formUtilsService.toPersonFormModel([this.formInstance, model]);
  }

  public buildForm(disabled: boolean = false): void {
    this.formInstance = this.fb.group({
      administrators: this.fb.array([], FormArrayValidators.atLeast(1))
    });
  }
}
