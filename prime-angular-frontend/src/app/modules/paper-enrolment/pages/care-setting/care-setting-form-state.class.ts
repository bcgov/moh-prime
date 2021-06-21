import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrollee } from '@shared/models/enrollee.model';
import { ConfigService } from '@config/config.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';

export class CareSettingFormState extends AbstractFormState<Enrolment> {
  public constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private configService: ConfigService
  ) {
    super();

    this.buildForm();
  }

  public get json(): Enrolment {
    if (!this.formInstance) {
      return;
    }

    // TODO adapt the data after getting values, ie. address(es)

    return this.formInstance.getRawValue();
  }

  public patchValue(enrolment: Enrolment): void {
    if (!this.formInstance) {
      return;
    }

    if (enrolment.careSettings.length) {
      const careSettings = this.formInstance.get('careSettings') as FormArray;
      careSettings.clear();
      enrolment.careSettings.forEach((s: CareSetting) => {
        const careSetting = this.buildCareSettingForm();
        careSetting.patchValue(s);
        careSettings.push(careSetting);
      });
    }

    // Initialize Health Authority form even if it might not be used by end user:
    // Create checkboxes for each known Health Authority, according to order of Health Authority list.
    const enrolleeHealthAuthorities = this.formInstance.get('enrolleeHealthAuthorities') as FormArray;
    enrolleeHealthAuthorities.clear();
    // Set value of checkboxes according to previous selections, if any
    this.configService.healthAuthorities.forEach(ha => {
      const checked = enrolment.enrolleeHealthAuthorities.some(eha => ha.code === eha.healthAuthorityCode);
      enrolleeHealthAuthorities.push(this.buildEnrolleeHealthAuthorityFormControl(checked));
    });

    this.formInstance.patchValue(enrolment.careSettings);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      careSettings: this.fb.array([]),
      enrolleeHealthAuthorities: this.fb.array([])
    });
  }

  public buildCareSettingForm(code: number = null): FormGroup {
    return this.fb.group({
      careSettingCode: [code, [Validators.required]]
    });
  }

  public buildEnrolleeHealthAuthorityFormControl(checkState: boolean): FormControl {
    return this.fb.control(checkState);
  }

  public removeHealthAuthorities() {
    const enrolleeHealthAuthorities = this.formInstance.get('enrolleeHealthAuthorities') as FormArray;
    enrolleeHealthAuthorities.controls.forEach(checkbox => {
      checkbox.setValue(false);
    });
  }

  public convertCareSettingFormToJson(enrolleeId: number): any {
    // Variable names must match keys for FormArrays in the FormGroup to get values
    // tslint:disable-next-line:prefer-const
    let { careSettings, enrolleeHealthAuthorities } = this.formInstance.getRawValue();

    // Any checked HA is converted into an enrollee health authority object literal,
    // which is used to create the payload to back-end
    enrolleeHealthAuthorities = enrolleeHealthAuthorities.reduce((selectedHealthAuthorities, checked, i) => {
      if (checked) {
        selectedHealthAuthorities.push({
          enrolleeId,
          healthAuthorityCode: this.configService.healthAuthorities[i].code
        });
      }
      return selectedHealthAuthorities;
    }, []);
    return { careSettings, enrolleeHealthAuthorities };
  }
}
