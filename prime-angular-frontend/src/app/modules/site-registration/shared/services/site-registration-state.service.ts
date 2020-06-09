import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';
import { RouterEvent } from '@angular/router';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { LoggerService } from '@core/services/logger.service';
import { RouteStateService } from '@core/services/route-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';
import { Address } from '@shared/models/address.model';

import { Site } from '@registration/shared/models/site.model';
import { Party } from '@registration/shared/models/party.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { SiteRoutes } from '@registration/site-registration.routes';
import { RemoteUserLocation } from '../models/remote-user-location.model';

@Injectable({
  providedIn: 'root'
})
export class SiteRegistrationStateService {
  public organizationInformationForm: FormGroup;
  public organizationTypeForm: FormGroup;
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public remoteUsersForm: FormGroup;
  public vendorForm: FormGroup;
  public signingAuthorityForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public administratorPharmaNetForm: FormGroup;
  public technicalSupportForm: FormGroup;

  private patched: boolean;
  private siteId: number;
  private provisionerId: number;
  private locationId: number;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    this.organizationInformationForm = this.buildOrganizationInformationForm();
    this.organizationTypeForm = this.buildOrganizationTypeForm();
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.remoteUsersForm = this.buildRemoteUsersForm();
    this.vendorForm = this.buildVendorForm();
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.administratorPharmaNetForm = this.buildAdministratorPharmaNetForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();

    // Initial state of the form is unpatched and ready for
    // site information
    this.patched = false;

    // TODO not needed until site review expands outwards from a single site
    // Listen for a route end that is outside of site review, and
    // reset the form model
    // this.routeStateService.onNavigationEnd()
    //   .subscribe((event: RouterEvent) => {
    //     const route = event.url.slice(event.url.lastIndexOf('/') + 1);
    //     if (!SiteRoutes.registrationRoutes().includes(route)) {
    //       this.logger.info('RESET SITE FORM');
    //       this.forms.forEach((form: FormGroup) => form.reset());
    //       this.patched = false;
    //     }
    //   });
  }

  public get isPatched() {
    return this.patched;
  }

  /**
   * @description
   * Store the site JSON and populate the site form, which can
   * only be set more than once when explicitly forced.
   */
  public setSite(site: Site, force: boolean = false) {
    if (!this.patched || force) {
      // Indicate that the form is patched, and may contain unsaved information
      this.patched = true;

      this.siteId = site?.id;
      this.provisionerId = site?.provisionerId;
      this.locationId = site?.locationId;

      this.patchSite(site);
    }
  }

  /**
   * @description
   * Get the site as JSON for submission.
   */
  // TODO use Partial<Site>
  public get site(): Site {
    const organizationInformation = this.organizationInformationForm.getRawValue();
    const physicalAddress = this.siteAddressForm.getRawValue();
    const { businessDays: businessHours } = this.hoursOperationForm.getRawValue();
    const { organizationTypeCode } = this.organizationTypeForm.getRawValue();
    const { remoteUsers } = this.remoteUsersForm.getRawValue();
    const vendor = this.vendorForm.getRawValue();


    // Adapt data for backend consumption
    if (!organizationInformation.id) {
      organizationInformation.id = 0;
    }
    if (!physicalAddress.id) {
      physicalAddress.id = 0;
    }
    if (!vendor.id) {
      vendor.id = 0;
    }

    let signingAuthority = this.signingAuthorityForm.getRawValue();
    if (!signingAuthority.id) {
      signingAuthority.id = 0;
    }
    // if (!signingAuthority.firstName) {
    //   signingAuthority = null;
    // } else
    if (!signingAuthority.mailingAddress.street) {
      signingAuthority.mailingAddress = null;
    } else if (!signingAuthority.mailingAddress.id) {
      signingAuthority.mailingAddress.id = 0;
    }


    const [
      administratorPharmaNet,
      privacyOfficer,
      technicalSupport
    ] = [
      this.administratorPharmaNetForm.getRawValue(),
      this.privacyOfficerForm.getRawValue(),
      this.technicalSupportForm.getRawValue()
    ].map((party: Party) => {
      if (!party.id) {
        party.id = 0;
      }
      if (!party.firstName) {
        party = null;
      } else if (!party.physicalAddress.street) {
        party.physicalAddress = null;
      } else if (!party.physicalAddress.id) {
        party.physicalAddress.id = 0;
      }

      return party;
    });

    return {
      id: this.siteId,
      locationId: this.locationId,
      location: {
        id: this.locationId,
        privacyOfficerId: privacyOfficer?.id,
        privacyOfficer,
        administratorPharmaNetId: administratorPharmaNet?.id,
        administratorPharmaNet,
        technicalSupportId: technicalSupport?.id,
        technicalSupport,
        organizationId: organizationInformation?.id,
        organization: {
          signingAuthorityId: signingAuthority?.id,
          signingAuthority,
          organizationTypeCode,
          ...organizationInformation
        },
        physicalAddressId: physicalAddress?.id,
        physicalAddress,
        businessHours
      },
      vendorId: vendor?.id,
      vendor,
      provisionerId: this.provisionerId,
      remoteUsers
      // TODO where is PEC coming from?
      // pec
    } as Site; // Force type definition
  }

  public get isDirty(): boolean {
    return this.forms.reduce(
      (isDirty: boolean, form: FormGroup) => isDirty || form.dirty, false
    );
  }

  public markAsPristine(): void {
    this.forms.forEach((form: FormGroup) => form.markAsPristine());
  }

  public isSiteValid(): boolean {
    return (
      this.isOrganizationInformationValid() &&
      this.isOrganizationTypeValid() &&
      this.isSiteAddressValid() &&
      this.isHoursOperationValid() &&
      this.isVendorValid() &&
      this.isSigningAuthorityValid() &&
      this.isPrivacyOfficerValid() &&
      this.isAdministratorValid() &&
      this.isTechnicalSupportValid()
    );
  }

  public isOrganizationInformationValid(): boolean {
    return this.organizationInformationForm.valid;
  }

  public isOrganizationTypeValid(): boolean {
    return this.organizationTypeForm.valid;
  }

  public isSiteAddressValid(): boolean {
    return this.siteAddressForm.valid;
  }

  public isHoursOperationValid(): boolean {
    return this.hoursOperationForm.valid;
  }

  public isVendorValid(): boolean {
    return this.vendorForm.valid;
  }

  public isSigningAuthorityValid(): boolean {
    return this.signingAuthorityForm.valid;
  }

  public isPrivacyOfficerValid(): boolean {
    return this.privacyOfficerForm.valid;
  }

  public isAdministratorValid(): boolean {
    return this.administratorPharmaNetForm.valid;
  }

  public isTechnicalSupportValid(): boolean {
    return this.technicalSupportForm.valid;
  }

  /**
   * @description
   * Create an empty remote user form group, and patch
   * it with a remote user if provided.
   */
  public createEmptyRemoteUserFormAndPatch(remoteUser: RemoteUser = null): FormGroup {
    const group = this.remoteUserFormGroup() as FormGroup;
    if (remoteUser) {
      const { id, firstName, lastName, remoteUserLocations } = remoteUser;
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
    }

    return group;
  }

  private patchSite(site: Site) {
    if (site) {
      this.organizationInformationForm.patchValue(site.location.organization);
      this.organizationTypeForm.patchValue(site.location.organization);
      if (site.location.physicalAddress) {
        this.siteAddressForm.patchValue(site.location.physicalAddress);
      }
      if (site.vendor) {
        this.vendorForm.patchValue(site.vendor);
      }

      if (site.location.businessHours?.length) {
        const array = this.hoursOperationForm.get('businessDays') as FormArray;
        array.clear(); // Clear out existing indices
        this.formUtilsService.formArrayPush(array, site.location.businessHours);
      }

      if (site.remoteUsers?.length) {
        const form = this.remoteUsersForm;
        const remoteUsersFormArray = form.get('remoteUsers') as FormArray;
        remoteUsersFormArray.clear(); // Clear out existing indices

        // Omitted from payload, but provided in the form to allow for
        // validation to occur when "Have Remote Users" is toggled
        // TODO component-level add control on init and remove control on submission to drop from state service
        form.get('hasRemoteUsers').patchValue(!!site.remoteUsers.length);

        site.remoteUsers.map((remoteUser: RemoteUser) => {
          const group = this.createEmptyRemoteUserFormAndPatch(remoteUser);
          remoteUsersFormArray.push(group);
        });
      }

      [
        [this.administratorPharmaNetForm, site.location.administratorPharmaNet],
        [this.privacyOfficerForm, site.location.privacyOfficer],
        [this.technicalSupportForm, site.location.technicalSupport]
      ]
        .filter(([formGroup, data]: [FormGroup, Party]) => data)
        .forEach(([formGroup, data]: [FormGroup, Party]) => {
          const { physicalAddress, ...party } = data;

          formGroup.patchValue(party);

          const physicalAddressFormGroup = formGroup.get('physicalAddress');
          (physicalAddress)
            ? physicalAddressFormGroup.patchValue(physicalAddress)
            : physicalAddressFormGroup.reset();
        });

      [[this.signingAuthorityForm, site.location.organization.signingAuthority]]
        .filter(([formGroup, data]: [FormGroup, Party]) => data)
        .forEach(([formGroup, data]: [FormGroup, Party]) => {
          const { mailingAddress, ...party } = data;

          formGroup.patchValue(party);

          const mailingAddressFormGroup = formGroup.get('mailingAddress');
          (mailingAddress)
            ? mailingAddressFormGroup.patchValue(mailingAddress)
            : mailingAddressFormGroup.reset();
        });
    }
  }

  private get forms(): AbstractControl[] {
    return [
      this.organizationInformationForm,
      this.organizationTypeForm,
      this.siteAddressForm,
      this.hoursOperationForm,
      this.vendorForm,
      this.signingAuthorityForm,
      this.privacyOfficerForm,
      this.administratorPharmaNetForm,
      this.technicalSupportForm
    ];
  }

  private buildOrganizationInformationForm(): FormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      name: [
        null,
        [Validators.required]
      ],
      registrationId: [
        null,
        [Validators.required]
      ],
      doingBusinessAs: [
        null,
        []
      ]
    });
  }

  private buildOrganizationTypeForm(code: number = null): FormGroup {
    return this.fb.group({
      organizationTypeCode: [code, [Validators.required]]
    });
  }

  private buildSiteAddressForm(): FormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      street: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      city: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      provinceCode: [
        { value: Province.BRITISH_COLUMBIA, disabled: true },
        [Validators.required]
      ],
      postal: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      countryCode: [
        { value: Country.CANADA, disabled: true },
        [Validators.required]
      ],
    });
  }

  private buildHoursOperationForm(): FormGroup {
    return this.fb.group({
      businessDays: this.fb.array(
        [],
        [Validators.required])
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
        // TODO at least one if has remote users if hasRemoteUsers is checked validator
        []
      )
    });
  }

  private remoteUserFormGroup(): FormGroup {
    return this.fb.group({
      id: [
        null,
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
      remoteUserLocations: this.fb.array(
        [],
        [FormArrayValidators.atLeast(1)]
      )
    });
  }

  public remoteUserLocationFormGroup(): FormGroup {
    return this.fb.group({
      id: [
        null,
        []
      ],
      internetProvider: [
        null,
        [Validators.required]
      ],
      physicalAddress: this.physicalAddressFormGroup(true)
    });
  }

  private buildVendorForm(): FormGroup {
    return this.fb.group({
      id: [
        0,
        [Validators.required]
      ]
    });
  }

  private buildSigningAuthorityForm(): FormGroup {
    // Prevent BCSC information from being changed
    const disabled = true;
    return this.fb.group({
      id: [null, []],
      firstName: [
        { value: null, disabled },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled },
        [Validators.required]
      ],
      preferredFirstName: [null, []],
      preferredMiddleName: [null, []],
      preferredLastName: [null, []],
      jobRoleTitle: [null, [Validators.required]],
      phone: [null, [Validators.required, FormControlValidators.phone]],
      fax: [null, [Validators.required, FormControlValidators.phone]],
      smsPhone: [null, [Validators.required, FormControlValidators.phone]],
      email: [null, [Validators.required, FormControlValidators.email]],
      physicalAddress: this.fb.group({
        id: [null, []],
        countryCode: [{ value: null, disabled }, []],
        provinceCode: [{ value: null, disabled }, []],
        street: [{ value: null, disabled }, []],
        street2: [{ value: null, disabled }, []],
        city: [{ value: null, disabled }, []],
        postal: [{ value: null, disabled }, []]
      }),
      mailingAddress: this.fb.group({
        id: [null, []],
        countryCode: [{ value: null, disabled: false }, []],
        provinceCode: [{ value: null, disabled: false }, []],
        street: [{ value: null, disabled: false }, []],
        street2: [{ value: null, disabled: false }, []],
        city: [{ value: null, disabled: false }, []],
        postal: [{ value: null, disabled: false }, []]
      }),
    });
  }

  private buildPrivacyOfficerForm(): FormGroup {
    return this.partyFormGroup();
  }

  private buildAdministratorPharmaNetForm(): FormGroup {
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
        [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      fax: [
        null,
        [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      smsPhone: [
        null,
        [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      email: [
        null,
        [
          Validators.required,
          FormControlValidators.email
        ]
      ],
      physicalAddress: this.physicalAddressFormGroup()
    });
  }

  private physicalAddressFormGroup(isRequired: boolean = false, disable: string[] = []): FormGroup {
    const validators = (isRequired) ? [Validators.required] : [];

    return this.fb.group({
      id: [
        // TODO should this be 0, or null like everything else?
        0,
        []
      ],
      countryCode: [
        { value: null, disabled: disable.includes('countryCode') },
        validators
      ],
      provinceCode: [
        { value: null, disabled: disable.includes('provinceCode') },
        validators
      ],
      street: [
        { value: null, disabled: disable.includes('street') },
        validators
      ],
      street2: [
        { value: null, disabled: disable.includes('street2') },
        // NOTE: Never used so omitted from validations to reduce need
        // to clear validators at the component-level
        []
      ],
      city: [
        { value: null, disabled: disable.includes('city') },
        validators
      ],
      postal: [
        { value: null, disabled: disable.includes('postal') },
        validators
      ]
    });
  }
}
