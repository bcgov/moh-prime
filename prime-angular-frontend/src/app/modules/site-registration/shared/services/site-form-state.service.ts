import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { StringUtils } from '@lib/utils/string-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormGroupValidators } from '@lib/validators/form-group.validators';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { Party } from '@registration/shared/models/party.model';
import { Site } from '@registration/shared/models/site.model';
import { AbstractFormState } from '@registration/shared/classes/abstract-form-state.class';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { RemoteUserLocation } from '@registration/shared/models/remote-user-location.model';
import { RemoteUserCertification } from '@registration/shared/models/remote-user-certification.model';
import { BusinessDay } from '@registration/shared/models/business-day.model';
import { BusinessDayHours } from '@registration/shared/models/business-day-hours.model';

@Injectable({
  providedIn: 'root'
})
export class SiteFormStateService extends AbstractFormState<Site> {
  public careSettingTypeForm: FormGroup;
  public businessForm: FormGroup;
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public remoteUsersForm: FormGroup;
  public administratorPharmaNetForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public technicalSupportForm: FormGroup;

  private siteId: number;
  private organizationId: number;
  private provisionerId: number;

  constructor(
    protected fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super(fb);
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(site: Site, forcePatch: boolean = false) {
    // Store required site identifiers not captured in forms
    this.siteId = site.id;
    this.organizationId = site.organizationId;
    this.provisionerId = site.provisionerId;

    super.setForm(site, forcePatch);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Site {
    const { careSettingCode, vendorCode } = this.careSettingTypeForm.getRawValue();
    const { doingBusinessAs } = this.businessForm.getRawValue();
    const { physicalAddress } = this.siteAddressForm.getRawValue();
    const businessHours = this.hoursOperationForm.getRawValue().businessDays
      .map((hours: BusinessDayHours, dayOfWeek: number) => {
        if (hours.startTime && hours.endTime) {
          hours.startTime = StringUtils.splice(hours.startTime, 2, ':');
          hours.endTime = StringUtils.splice(hours.endTime, 2, ':');
        }
        return new BusinessDay(dayOfWeek, hours.startTime, hours.endTime);
      })
      .filter((day: BusinessDay) => day.startTime !== null);
    const remoteUsers = this.remoteUsersForm.getRawValue().remoteUsers
      .map((ru: RemoteUser) => {
        // Remove the ID from the remote user to simplify updates on the server
        const { id, ...remoteUser } = ru;
        return remoteUser;
      });
    const [administratorPharmaNet, privacyOfficer, technicalSupport] = [
      this.administratorPharmaNetForm.getRawValue(),
      this.privacyOfficerForm.getRawValue(),
      this.technicalSupportForm.getRawValue()
    ].map((party: Party) => this.toPartyJson(party));

    // Includes site related keys to uphold relationships, and allow for updates
    // to a site. Keys not for update have been omitted and the type enforced
    return {
      id: this.siteId,
      organizationId: this.organizationId,
      // organization (N/A)
      provisionerId: this.provisionerId,
      // provisioner (N/A)
      careSettingCode,
      // Only using single vendors for now
      siteVendors: [{
        siteId: this.siteId,
        vendorCode
      }],
      // businessLicenceDocuments (N/A)
      doingBusinessAs,
      physicalAddressId: physicalAddress?.id,
      physicalAddress,
      businessHours,
      remoteUsers,
      administratorPharmaNetId: administratorPharmaNet?.id,
      administratorPharmaNet,
      privacyOfficerId: privacyOfficer?.id,
      privacyOfficer,
      technicalSupportId: technicalSupport?.id,
      technicalSupport,
      // completed (N/A)
      // approvedDate (N/A)
      // submittedDate (N/A)
      // pec (N/A)
    } as Site; // Enforced type
  }

  /**
   * @description
   * Helper for getting a list of organization forms.
   */
  public get forms(): AbstractControl[] {
    return [
      this.careSettingTypeForm,
      this.businessForm,
      this.siteAddressForm,
      this.hoursOperationForm,
      this.remoteUsersForm,
      this.administratorPharmaNetForm,
      this.privacyOfficerForm,
      this.technicalSupportForm
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  public init() {
    this.careSettingTypeForm = this.buildCareSettingTypeForm();
    this.businessForm = this.buildBusinessForm();
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.remoteUsersForm = this.buildRemoteUsersForm();
    this.administratorPharmaNetForm = this.buildAdministratorPharmaNetForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  protected patchForm(site: Site): Site {
    if (!site) {
      return null;
    }

    this.careSettingTypeForm.patchValue(site);

    if (site.siteVendors?.length) {
      this.careSettingTypeForm.get('vendorCode').patchValue(site.siteVendors[0].vendorCode);
    }

    if (site.doingBusinessAs) {
      this.businessForm.get('doingBusinessAs').patchValue(site.doingBusinessAs);
    }

    if (site.physicalAddress) {
      this.siteAddressForm.get('physicalAddress').patchValue(site.physicalAddress);
    }

    if (site.businessHours?.length) {
      const businessDays = [...Array(7).keys()]
        .reduce((days: (BusinessDay | {})[], dayOfWeek: number) => {
          const day = site.businessHours.find(bh => bh.day === dayOfWeek);
          if (day) {
            day.startTime = day.startTime.replace(':', '');
            day.endTime = day.endTime.replace(':', '');
          }
          days.push(day ?? {});
          return days;
        }, []);
      this.hoursOperationForm.get('businessDays').patchValue(businessDays);
    }

    if (site.remoteUsers?.length) {
      const form = this.remoteUsersForm;
      const remoteUsersFormArray = form.get('remoteUsers') as FormArray;
      remoteUsersFormArray.clear(); // Clear out existing indices

      // Omitted from payload, but provided in the form to allow for
      // validation to occur when "Have Remote Users" is toggled
      form.get('hasRemoteUsers').patchValue(!!site.remoteUsers.length);

      site.remoteUsers.map((remoteUser: RemoteUser) => {
        const group = this.createEmptyRemoteUserFormAndPatch(remoteUser);
        remoteUsersFormArray.push(group);
      });
    }

    [
      [this.administratorPharmaNetForm, site.administratorPharmaNet],
      [this.privacyOfficerForm, site.privacyOfficer],
      [this.technicalSupportForm, site.technicalSupport]
    ]
      .filter(([form, data]: [FormGroup, Party]) => data)
      .forEach((formParty: [FormGroup, Party]) => this.toPartyFormModel(formParty));
  }

  /**
   * @description
   * Create an empty remote user form group, and patch
   * it with a remote user if provided.
   */
  public createEmptyRemoteUserFormAndPatch(remoteUser: RemoteUser = null): FormGroup {
    const group = this.remoteUserFormGroup() as FormGroup;
    if (remoteUser) {
      const { id, firstName, lastName, remoteUserLocations, remoteUserCertifications } = remoteUser;
      group.patchValue({ id, firstName, lastName });
      const array = group.get('remoteUserLocations') as FormArray;
      remoteUserLocations
        .map((rul: RemoteUserLocation) => {
          const formGroup = this.remoteUserLocationFormGroup();
          formGroup.patchValue(rul);
          return formGroup;
        })
        .forEach((remoteUserLocationFormGroup: FormGroup) =>
          array.push(remoteUserLocationFormGroup)
        );

      const certs = group.get('remoteUserCertifications') as FormArray;
      remoteUserCertifications.map((cert: RemoteUserCertification) => {
        const formGroup = this.remoteUserCertificationFormGroup();
        formGroup.patchValue(cert);
        return formGroup;
      })
        .forEach((remoteUserLocationFormGroup: FormGroup) =>
          certs.push(remoteUserLocationFormGroup)
        );
    }

    return group;
  }

  private buildCareSettingTypeForm(code: number = null): FormGroup {
    return this.fb.group({
      careSettingCode: [
        code,
        [Validators.required]
      ],
      vendorCode: [
        0,
        [FormControlValidators.requiredIndex]
      ]
    });
  }

  private buildBusinessForm(): FormGroup {
    return this.fb.group({
      doingBusinessAs: [
        '',
        []
      ]
    });
  }

  private buildSiteAddressForm(): FormGroup {
    return this.fb.group({
      physicalAddress: this.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        areDisabled: ['provinceCode', 'countryCode'],
        useDefaults: true,
        exclude: ['street2']
      })
    });
  }

  private buildHoursOperationForm(): FormGroup {
    const groups = [...new Array(7)].map(() =>
      this.fb.group({
        startTime: [null, []],
        endTime: [null, []],
      }, { validator: FormGroupValidators.lessThan('startTime', 'endTime') })
    );

    return this.fb.group({
      businessDays: this.fb.array(groups)
      // TODO at least one business hours is required
      // [FormArrayValidators.atLeast(1)]
    });
  }

  private buildRemoteUsersForm(): FormGroup {
    return this.fb.group({
      // Omitted from payload, but provided in the form to allow for
      // validation to occur when "Have Remote Users" is toggled
      hasRemoteUsers: [
        false,
        []
      ],
      remoteUsers: this.fb.array(
        [],
        []
      )
      // TODO at least one remote users is required
      // [FormArrayValidators.atLeast(1)]
    });
  }

  private remoteUserFormGroup(): FormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        null,
        [Validators.required]
      ],
      lastName: [
        null,
        [Validators.required]
      ],
      remoteUserCertifications: this.fb.array([]),
      remoteUserLocations: this.fb.array(
        [],
        [FormArrayValidators.atLeast(1)]
      )
    });
  }

  public remoteUserLocationFormGroup(): FormGroup {
    return this.fb.group({
      internetProvider: [
        null,
        [Validators.required]
      ],
      physicalAddress: this.buildAddressForm({
        areRequired: ['street', 'city', 'provinceCode', 'countryCode', 'postal'],
        exclude: ['street2']
      })
    });
  }

  public remoteUserCertificationFormGroup(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', []],
      // Validators are applied at the component-level when
      // fields are made visible to allow empty submissions
      licenseNumber: [null, []],
    });
  }

  private buildAdministratorPharmaNetForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildPrivacyOfficerForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildTechnicalSupportForm(): FormGroup {
    return this.partyFormGroup();
  }

  private partyFormGroup(disabled: boolean = false): FormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        { value: null, disabled },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled },
        [Validators.required]
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      phone: [
        null,
        [Validators.required, FormControlValidators.phone]
      ],
      fax: [
        null,
        [FormControlValidators.phone]
      ],
      smsPhone: [
        null,
        [FormControlValidators.phone]
      ],
      email: [
        null,
        [Validators.required, FormControlValidators.email]
      ],
      physicalAddress: this.buildAddressForm({
        exclude: ['street2']
      })
    });
  }
}
