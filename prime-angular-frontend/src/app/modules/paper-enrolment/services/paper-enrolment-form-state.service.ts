import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ConfigService } from '@config/config.service';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { RegulatoryFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { DemographicFormState } from '@paper-enrolment/pages/demographic-page/demographic-form-state.class';
import { CareSettingFormState } from '@paper-enrolment/pages/care-setting-page/care-setting-form-state.class';
import { OboSiteFormState } from '@paper-enrolment/pages/obo-sites-page/obo-sites-form-state.class';
import { SelfDeclarationFormState } from '@paper-enrolment/pages/self-declaration-page/self-declaration-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class PaperEnrolmentFormStateService extends AbstractFormStateService<Enrolment> {
  public accessForm: FormGroup;
  public demographicFormState: DemographicFormState;
  public careSettingFormState: CareSettingFormState;
  public regulatoryFormState: RegulatoryFormState;
  public jobsFormState: OboSiteFormState;
  public selfDeclarationFormState: SelfDeclarationFormState;
  public accessAgreementForm: FormGroup;

  private enrolleeId: number;
  private userId: string;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: LoggerService,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService
  ) {
    super(fb, routeStateService, logger);

    this.initialize([...EnrolmentRoutes.enrolmentProfileRoutes()]);
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   *
   * NOTE: Executed by views to populate their form models, which
   * allows for it to be used for setting required values that
   * can't be loaded during instantiation.
   */
  public async setForm(enrolment: Enrolment, forcePatch: boolean = false): Promise<void> {
    if (!enrolment) {
      return;
    }

    // Store required enrolment identifiers not captured in forms
    this.enrolleeId = enrolment?.id;
    this.userId = enrolment?.enrollee.userId;

    super.setForm(enrolment, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Enrolment {
    const id = this.enrolleeId;
    const userId = this.userId;
    const profile = this.demographicFormState.json;
    const certifications = this.regulatoryFormState.json;
    const { oboSites } = this.jobsFormState.json;
    const careSettings = this.careSettingFormState.convertCareSettingFormToJson(id);
    // const selfDeclarations = this.selfDeclarationFormState.convertSelfDeclarationsToJson(id);
    const { accessAgreementGuid } = this.accessAgreementForm.getRawValue();

    return {
      id,
      enrollee: {
        userId,
        ...profile
      },
      certifications,
      oboSites,
      ...careSettings,
      selfDeclarations,
      accessAgreementGuid
    };
  }

  /**
   * @description
   * Helper for getting a list of enrolment forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.demographicFormState.form,
      this.careSettingFormState.form,
      this.regulatoryFormState.form,
      this.jobsFormState.form,
      this.selfDeclarationFormState.form,
    ];
  }

  /**
   * @description
   * Check that all constituent forms are valid.
   */
  public get isValid(): boolean {
    return super.isValid && this.hasCertificateOrJob();
  }

  /**
   * @description
   * Check for the requirement of at least one certification, or one obo site/job.
   */
  public hasCertificateOrJob(): boolean {
    const oboSites = this.jobsFormState.oboSites;
    const certifications = this.regulatoryFormState.certifications;
    const enrolleeHealthAuthorities = this.careSettingFormState.form.get('enrolleeHealthAuthorities') as FormArray;
    let hasOboSiteForEveryHA = true;
    // Using `for` loop rather than `every()` method for ease of debugging
    for (let i = 0; i < enrolleeHealthAuthorities.controls.length; i++) {
      const checkbox = enrolleeHealthAuthorities.controls[i];
      // For every selected Health Authority ...
      if (checkbox.value === true) {
        let foundMatchingHAOboSite = false;
        // ... there must be at least one Obo Site for that Health Authority
        for (let j = 0; j < oboSites.controls.length; j++) {
          const oboSiteForm = oboSites.controls[j] as FormGroup;
          if (oboSiteForm.controls.healthAuthorityCode.value === this.configService.healthAuthorities[i].code) {
            foundMatchingHAOboSite = true;
            break;
          }
        }
        if (!foundMatchingHAOboSite) {
          hasOboSiteForEveryHA = false;
          break;
        }
      }
    }
    // When you set certifications to 'None' there still exists an item in
    // the FormArray, and this checks for its existence
    return (oboSites.length && hasOboSiteForEveryHA) || (certifications.length && certifications.value[0].licenseNumber);
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  protected buildForms() {
    this.demographicFormState = new DemographicFormState(this.fb, this.formUtilsService);
    this.careSettingFormState = new CareSettingFormState(this.fb, this.configService);
    this.regulatoryFormState = new RegulatoryFormState(this.fb);
    this.jobsFormState = new OboSiteFormState(this.fb, this.formUtilsService, this.configService);
    // this.selfDeclarationFormState = new SelfDeclarationFormState(this.fb, this.formUtilsService, this.configService);
    this.accessAgreementForm = this.buildAccessAgreementForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(enrolment: Enrolment) {
    if (!enrolment) {
      return;
    }

    // this.demographicFormState.patchValue(enrolment.enrollee);
    this.careSettingFormState.patchValue(enrolment);
    this.regulatoryFormState.patchValue(enrolment.certifications);
    this.jobsFormState.patchValue(enrolment);
    this.selfDeclarationFormState.patchValue(enrolment);

    // After patching the form is dirty, and needs to be pristine
    // to allow for deactivation modals to work properly
    this.markAsPristine();
  }

  /**
   * @description
   * Determine whether the form should be reset based
   * on the current route path.
   */
  protected checkResetRoutes(currentRoutePath: string, resetRoutes: string[]): boolean {
    return !resetRoutes?.includes(currentRoutePath);
  }

  /**
   * Form Builders and Helpers
   */
  private buildAccessAgreementForm(): FormGroup {
    return this.fb.group({
      accessAgreementGuid: [
        '',
        []
      ]
    });
  }

}
