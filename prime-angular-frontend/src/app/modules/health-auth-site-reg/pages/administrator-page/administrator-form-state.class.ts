import { FormBuilder } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AdministratorForm } from './administrator-form.model';

export class AdministratorFormState extends AbstractFormState<AdministratorForm> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super();

    this.buildForm();
  }

  public get json(): AdministratorForm {
    if (!this.formInstance) {
      return;
    }

    // return this.formUtilsService.toPersonJson(this.formInstance.getRawValue());
  }

  public patchValue(model: AdministratorForm): void {
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
