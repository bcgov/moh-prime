import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';

import { Party } from '@registration/shared/models/party.model';
import { Site } from '@registration/shared/models/site.model';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { RemoteUserLocation } from '@registration/shared/models/remote-user-location.model';

// TODO default is null and on reset it would be great if no special 0 id was required
// TODO add a form state service interface/abstract class for form state services
// TODO should the forms built be stored in a different file or service
@Injectable({
  providedIn: 'root'
})
export class SiteFormStateService {
  // TODO rename this member variable since it isn't just site anymore
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public vendorForm: FormGroup;
  public remoteUsersForm: FormGroup;
  public administratorPharmaNetForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public technicalSupportForm: FormGroup;

  // ***************************************************
  // TODO temporarily added to get this to work for demo
  public tempSite: Site;
  // ***************************************************

  private patched: boolean;
  private siteId: number;
  private locationId: number;
  private organizationId: number;
  private provisionerId: number;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    // Initial state of the form is unpatched and ready for
    // enrolment information
    this.patched = false;

    // Initialize and configure the forms
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.vendorForm = this.buildVendorForm();
    this.remoteUsersForm = this.buildRemoteUsersForm();
    this.administratorPharmaNetForm = this.buildAdministratorPharmaNetForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(site: Site, forcePatch: boolean = false) {
    if (this.patched && !forcePatch) {
      return;
    }

    // Indicate that the form is patched, and may contain unsaved information
    this.patched = true;

    // Store required site identifiers not captured in forms
    this.siteId = site.id;
    this.locationId = site.location.id;
    this.organizationId = site.location.organizationId;
    this.provisionerId = site.provisionerId;

    // ***************************************************
    // TODO temporarily added to get this to work for demo
    this.tempSite = site;
    // ***************************************************

    this.patchForm(site);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  // TODO method constructs the JSON, and attempts to adapt, should
  // adapt in only one place and separately in method
  public get site(): Site {
    const { name, physicalAddress } = this.siteAddressForm.getRawValue();
    const businessHours = this.hoursOperationForm.getRawValue().businessDays;
    const vendor = this.vendorForm.getRawValue();
    const { remoteUsers } = this.remoteUsersForm.getRawValue();

    const [
      administratorPharmaNet,
      privacyOfficer,
      technicalSupport
    ] = [
      this.administratorPharmaNetForm.getRawValue(),
      this.privacyOfficerForm.getRawValue(),
      this.technicalSupportForm.getRawValue()
    ].map((party: Party) => {
      if (!party.firstName) {
        party = null;
      } else if (!party.physicalAddress.street) {
        party.physicalAddress = null;
      }

      // ***************************************************
      // TODO temporarily added to get this to work for demo
      if (party) {
        party.hpdid = this.tempSite?.provisioner.hpdid;
        party.userId = this.tempSite?.provisioner.userId;
      }
      // ***************************************************

      return party;
    });

    // Includes site and location related keys to uphold relationships, and
    // allow for updates to a site. Keys not for update have been omitted
    // and the type enforced
    return {
      id: this.siteId,
      name,
      provisionerId: this.provisionerId,
      // provisioner (N/A)
      locationId: this.locationId,
      location: {
        id: this.locationId,
        organizationId: this.organizationId,
        // TODO set on organization and copied to location, but why?
        // TODO not going to work as they expect regarding site name
        // doingBusinessAs
        physicalAddressId: physicalAddress?.id,
        physicalAddress,
        businessHours,
        administratorPharmaNetId: administratorPharmaNet?.id,
        administratorPharmaNet,
        privacyOfficerId: privacyOfficer?.id,
        privacyOfficer,
        technicalSupportId: technicalSupport?.id,
        technicalSupport
      },
      vendorId: vendor?.id,
      vendor,
      remoteUsers,
      // TODO pec not implemented
      // completed (N/A)
      // approvedDate (N/A)
      // submittedDate (N/A)
    } as Site; // Enforced type
  }

  public get isValid(): boolean {
    return this.forms
      .reduce((valid: boolean, form: AbstractControl) => valid && form.valid, true);
  }

  public get isDirty(): boolean {
    return this.forms
      .reduce((dirty: boolean, form: AbstractControl) => dirty || form.dirty, false);
  }

  public markAsPristine(): void {
    this.forms
      .forEach((form: AbstractControl) => form.markAsPristine());
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

  /**
   * @description
   * Helper for getting a list of organization forms.
   */
  private get forms(): AbstractControl[] {
    return [
      this.siteAddressForm,
      this.hoursOperationForm,
      this.vendorForm,
      this.remoteUsersForm,
      this.administratorPharmaNetForm,
      this.privacyOfficerForm,
      this.technicalSupportForm
    ];
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  // TODO refactor into separate adapters
  private patchForm(site: Site): Site {
    if (!site) {
      return null;
    }

    this.siteAddressForm.get('name').patchValue(site.name);

    if (site.location.physicalAddress) {
      console.log(site.location.physicalAddress);

      this.siteAddressForm.get('physicalAddress').patchValue(site.location.physicalAddress);
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

    // TODO duplicated until services are completely split apart
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
  }

  // TODO rename this method since it isn't just address anymore
  private buildSiteAddressForm(): FormGroup {
    return this.fb.group({
      name: [
        null,
        [Validators.required]
      ],
      physicalAddress: this.fb.group({
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
        ]
      })
    });
  }

  private buildHoursOperationForm(): FormGroup {
    return this.fb.group({
      businessDays: this.fb.array(
        [],
        [Validators.required])
    });
  }

  private buildVendorForm(): FormGroup {
    return this.fb.group({
      // TODO id can't be null, but might be worth adding a new custom required validator
      id: [
        0,
        // TODO can't be required since 0 is considered valid
        // TODO can't be made null due to issues updating the site
        // TODO make a required and not 0 validator
        // [Validators.required]
        // TODO using pattern for now that matches the IDs of the vendors should be updated to pull from config
        [Validators.pattern('[1-5]{1,}')]
      ]
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
      remoteUserLocations: this.fb.array(
        [],
        [FormArrayValidators.atLeast(1)]
      )
    });
  }

  public remoteUserLocationFormGroup(): FormGroup {
    return this.fb.group({
      id: [
        0,
        []
      ],
      internetProvider: [
        null,
        [Validators.required]
      ],
      physicalAddress: this.physicalAddressFormGroup(true)
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

  // TODO duplicated until services are completely split apart
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
      // TODO duplication split out into reuseable address model
      physicalAddress: this.fb.group({
        id: [
          0,
          []
        ],
        street: [
          { value: null, disabled: false },
          []
        ],
        // TODO not needed and can likely be removed
        street2: [
          { value: null, disabled: false },
          []
        ],
        city: [
          { value: null, disabled: false },
          []
        ],
        provinceCode: [
          { value: null, disabled: false },
          []
        ],
        countryCode: [
          { value: null, disabled: false },
          []
        ],
        postal: [
          { value: null, disabled: false },
          []
        ]
      })
    });
  }

  /**
   * @description
   * Provide an address form group.
   *
   * @param options available for manipulating the form group
   *  areRequired control names
   *  areDisabled control names
   *  useDefaults for province and country, otherwise empty
   */
  // TODO when everything is working then start sliding this into place
  private buildAddressForm(options?: {
    areRequired: string[],
    areDisabled: string[],
    useDefaults: boolean
  }): FormGroup {
    const controlsConfig = {
      id: [
        0,
        []
      ],
      street: [
        { value: null, disabled: false },
        []
      ],
      street2: [
        { value: null, disabled: false },
        []
      ],
      city: [
        { value: null, disabled: false },
        []
      ],
      provinceCode: [
        { value: null, disabled: false },
        []
      ],
      countryCode: [
        { value: null, disabled: false },
        []
      ],
      postal: [
        { value: null, disabled: false },
        []
      ]
    };

    Object.keys(controlsConfig).map((key: string, index: number) => {
      const control = controlsConfig[key];
      if (options.areDisabled.includes(key)) {
        control[0].disabled = true;
      }
      if (options.useDefaults) {
        if (key === 'provinceCode') {
          control[0].value = Province.BRITISH_COLUMBIA;
        } else if (key === 'countryCode') {
          control[0].value = Country.CANADA;
        }
      }
      if (options.areRequired.includes(key)) {
        control[1].push(Validators.required);
      }
    });

    return this.fb.group(controlsConfig);
  }

  /**
   * @deprecated drop and replace with buildAddressForm
   */
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
