import { FormArray, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ConfigService } from '@config/config.service';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingForm } from './care-setting-form.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Config } from '@config/config.model';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

export class CareSettingFormState extends AbstractFormState<CareSettingForm> {
  public constructor(
    private fb: FormBuilder,
    private configService: ConfigService
  ) {
    super();

    this.buildForm();
  }

  public get enrolleeCareSettings(): FormArray {
    return this.form.get('enrolleeCareSettings') as FormArray;
  }

  public get enrolleeHealthAuthorities(): FormArray {
    return this.form.get('enrolleeHealthAuthorities') as FormArray;
  }

  public get json(): any {
    if (!this.formInstance) {
      return;
    }

    // Variable names must match keys for FormArrays in the FormGroup to get values
    // tslint:disable-next-line:prefer-const
    let { enrolleeCareSettings, enrolleeHealthAuthorities } = this.formInstance.getRawValue();

    // Any checked HA is converted into an enrollee health authority object literal,
    const healthAuthorities = enrolleeHealthAuthorities.reduce((selectedHealthAuthorities, checked, i) => {
      if (checked) {
        selectedHealthAuthorities.push(this.configService.healthAuthorities[i].code);
      }
      return selectedHealthAuthorities;
    }, []);

    const careSettings = enrolleeCareSettings.map((careSetting: CareSetting) => careSetting.careSettingCode);

    return { careSettings, healthAuthorities };
  }

  public patchValue(pageModel: CareSettingForm): void {
    if (!this.formInstance) {
      return;
    }

    if (pageModel.enrolleeCareSettings.length) {
      const enrolleeCareSettings = this.formInstance.get('enrolleeCareSettings') as FormArray;
      enrolleeCareSettings.clear();
      pageModel.enrolleeCareSettings.forEach((s: CareSetting) => {
        const careSetting = this.buildCareSettingForm();
        careSetting.patchValue(s);
        enrolleeCareSettings.push(careSetting);
      });
    }

    this.enrolleeHealthAuthorities.clear();
    // Set value of checkboxes according to previous selections, if any
    this.configService.healthAuthorities.forEach(ha => {
      const checked = pageModel.enrolleeHealthAuthorities.some(eha => ha.code === eha.healthAuthorityCode);
      this.enrolleeHealthAuthorities.push(this.buildEnrolleeHealthAuthorityFormControl(checked));
    });

    this.formInstance.patchValue(pageModel.enrolleeCareSettings);
    this.setHealthAuthorityValidator();
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      enrolleeCareSettings: this.fb.array([]),
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
    this.enrolleeHealthAuthorities.controls
      .forEach(checkbox => checkbox.setValue(false));
  }

  public disableCareSetting(careSettingCode: number): boolean {
    return ![
      CareSettingEnum.COMMUNITY_PHARMACIST,
      CareSettingEnum.HEALTH_AUTHORITY,
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
    ].includes(careSettingCode);
  }

  public addCareSetting() {
    const careSetting = this.buildCareSettingForm();
    this.enrolleeCareSettings.push(careSetting);
    this.setHealthAuthorityValidator();
  }

  public removeCareSetting(index: number) {
    this.enrolleeCareSettings.removeAt(index);
    this.setHealthAuthorityValidator();
  }

  public hasSelectedHACareSetting(): boolean {
    return (this.enrolleeCareSettings.value.some(e => e.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY));
  }

  public filterCareSettingTypes(careSetting: FormGroup) {
    // Create a list of filtered care settings
    if (this.enrolleeCareSettings.length) {
      // All the currently chosen care settings
      const selectedCareSettingCodes = this.enrolleeCareSettings.value
        .map((cs: CareSetting) => cs.careSettingCode);
      // Current care setting selected
      const currentCareSetting = this.configService.careSettings
        .find(cs => cs.code === careSetting.get('careSettingCode').value);
      // Filter the list of possible care settings using the selected care setting
      const filteredCareSettingTypes = this.configService.careSettings
        .filter((c: Config<number>) => !selectedCareSettingCodes.includes(c.code));

      if (currentCareSetting) {
        // Add the current careSetting to the list of filtered
        // careSettings so it remains visible
        filteredCareSettingTypes.unshift(currentCareSetting);
      }

      return filteredCareSettingTypes;
    }

    // Otherwise, provide the entire list of care setting types
    return this.configService.careSettings;
  }

  public removeIncompleteCareSettings(): void {
    this.enrolleeCareSettings.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('careSettingCode').value;

        // Remove if care setting is empty or the group is invalid
        if (!value || control.invalid) {
          this.removeCareSetting(index);
        }
      });

    // Always have a single care setting available, and it prevents
    // the page from jumping too much when routing
    if (!this.enrolleeCareSettings.controls.length) {
      this.addCareSetting();
    }
  }

  private setHealthAuthorityValidator(): void {
    this.hasSelectedHACareSetting()
      ? this.enrolleeHealthAuthorities.setValidators(FormArrayValidators.atLeast(1, (control: FormControl) => control.value))
      : this.enrolleeHealthAuthorities.clearValidators()
  }

}
