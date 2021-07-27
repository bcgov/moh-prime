import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder } from '@angular/forms';

import { AbstractFormStateService } from '@lib/classes/abstract-form-state-service.class';
import { RouteStateService } from '@core/services/route-state.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LdapInformationPageFormState } from '@gis/pages/ldap-information-page/ldap-information-page-form-state.class';
import { LdapUserPageFormState } from '@gis/pages/ldap-user-page/ldap-user-page-form-state.class';
import { OrganizationInformationPageFormState } from '@gis/pages/organization-information-page/organization-information-page-form-state.class';
import { EnrolleeInformationPageFormState } from '@gis/pages/enrollee-information-page/enrollee-information-page-form-state.class';
import { GisEnrolment } from '../models/gis-enrolment.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

@Injectable({
  providedIn: 'root'
})
export class GisEnrolmentFormStateService extends AbstractFormStateService<GisEnrolment> {
  public ldapUserPageFormState: LdapUserPageFormState;
  public ldapInformationPageFormState: LdapInformationPageFormState;
  public organizationInformationPageFormState: OrganizationInformationPageFormState;
  public enrolleeInformationPageFormState: EnrolleeInformationPageFormState;

  private enrolmentId: number;
  private bcscUser: Omit<BcscUser, 'verifiedAddress'>;

  constructor(
    protected fb: FormBuilder,
    protected routeStateService: RouteStateService,
    protected logger: ConsoleLoggerService,
    private formUtilsService: FormUtilsService
  ) {
    super(fb, routeStateService, logger);

    this.initialize();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(enrolment: GisEnrolment, forcePatch: boolean = false) {
    if (!enrolment) {
      return;
    }

    // Store required enrolment identifiers not captured in forms
    this.enrolmentId = enrolment.id;
    // TODO temporary and should be merged into prior to update
    // TODO GisEnrolment should be a composite of two models
    // that contains BCSC and Enrolment information
    this.bcscUser = GisEnrolment.toBcscUser(enrolment);

    super.setForm(enrolment, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): GisEnrolment {
    const ldapInformation = this.ldapInformationPageFormState.json;
    const organizationInformation = this.organizationInformationPageFormState.json;
    const enrolleeInformation = this.enrolleeInformationPageFormState.json;

    return {
      id: this.enrolmentId,
      ...this.bcscUser,
      // ldapLoginSuccessDate (N/A)
      ...ldapInformation,
      ...organizationInformation,
      ...enrolleeInformation
      // submittedDate (N/A)
    } as GisEnrolment;
  }

  /**
   * @description
   * Helper for getting a list of enrolment forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.ldapUserPageFormState.form,
      this.ldapInformationPageFormState.form,
      this.organizationInformationPageFormState.form,
      this.enrolleeInformationPageFormState.form
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  protected buildForms(): void {
    this.ldapUserPageFormState = new LdapUserPageFormState(this.fb);
    this.ldapInformationPageFormState = new LdapInformationPageFormState(this.fb);
    this.organizationInformationPageFormState = new OrganizationInformationPageFormState(this.fb);
    this.enrolleeInformationPageFormState = new EnrolleeInformationPageFormState(this.fb);
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(model: GisEnrolment): void {
    if (!model) {
      return;
    }

    this.ldapUserPageFormState.patchValue(model);
    this.ldapInformationPageFormState.patchValue(model);
    this.organizationInformationPageFormState.patchValue(model);
    this.enrolleeInformationPageFormState.patchValue(model);

    // After patching the form is dirty, and needs to be pristine
    // to allow for deactivation modals to work properly
    this.markAsPristine();
  }
}
