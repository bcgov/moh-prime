import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ConfigService } from '@config/config.service';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingForm } from './care-setting-form.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Config } from '@config/config.model';
export class CareSettingFormState extends AbstractFormState<CareSettingForm> {
  public constructor(
    private fb: FormBuilder,
    private configService: ConfigService
  ) {
    super();

    this.buildForm();
  }

  public get careSettings(): FormArray {
    return this.form.get('careSettings') as FormArray;
  }

  public get enrolleeHealthAuthorities(): FormArray {
    return this.form.get('enrolleeHealthAuthorities') as FormArray;
  }

  public get json(): CareSettingForm {
    if (!this.formInstance) {
      return;
    }

    // TODO adapt the data after getting values, ie. address(es)

    return this.formInstance.getRawValue();
  }

  public patchValue(pageModel: CareSettingForm): void {
    if (!this.formInstance) {
      return;
    }

    if (pageModel.careSettings.length) {
      const careSettings = this.formInstance.get('careSettings') as FormArray;
      careSettings.clear();
      pageModel.careSettings.forEach((s: CareSetting) => {
        const careSetting = this.buildCareSettingForm();
        careSetting.patchValue(s);
        careSettings.push(careSetting);
      });
    }

    // Initialize Health Authority form even if it might not be used by end user:
    // Create checkboxes for each known Health Authority, according to order of Health Authority list.
    this.enrolleeHealthAuthorities.clear();
    // Set value of checkboxes according to previous selections, if any
    this.configService.healthAuthorities.forEach(ha => {
      const checked = pageModel.enrolleeHealthAuthorities.some(eha => ha.code === eha.healthAuthorityCode);
      this.enrolleeHealthAuthorities.push(this.buildEnrolleeHealthAuthorityFormControl(checked));
    });

    this.formInstance.patchValue(pageModel.careSettings);
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
    this.enrolleeHealthAuthorities.controls.forEach(checkbox => {
      checkbox.setValue(false);
    });
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
    this.careSettings.push(careSetting);
  }

  public removeCareSetting(index: number) {
    this.careSettings.removeAt(index);
  }

  public hasSelectedHACareSetting(): boolean {
    return (this.careSettings.value.some(e => e.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY));
  }

  public filterCareSettingTypes(careSetting: FormGroup) {
    // Create a list of filtered care settings
    if (this.careSettings.length) {
      // All the currently chosen care settings
      const selectedCareSettingCodes = this.careSettings.value
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

  public removeIncompleteCareSettings() {
    this.careSettings.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('careSettingCode').value;

        // Remove if care setting is empty or the group is invalid
        if (!value || control.invalid) {
          this.removeCareSetting(index);
        }
      });

    // Always have a single care setting available, and it prevents
    // the page from jumping too much when routing
    if (!this.careSettings.controls.length) {
      this.addCareSetting();
    }
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

    careSettings = careSettings.map((careSetting: CareSetting) => careSetting.careSettingCode);
    const healthAuthorities = enrolleeHealthAuthorities.map((enrolleeHealthAuthorities: EnrolleeHealthAuthority) => enrolleeHealthAuthorities.healthAuthorityCode);

    return { careSettings, healthAuthorities };
  }
}
