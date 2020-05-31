import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormArray, AbstractControl } from '@angular/forms';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';
import { Address } from '@shared/models/address.model';

import { Party } from '@registration/shared/models/party.model';
import { Site } from '@registration/shared/models/site.model';

@Injectable({
  providedIn: 'root'
})
export class SiteFormStateService {
  public siteAddressForm: FormGroup;
  public hoursOperationForm: FormGroup;
  public vendorForm: FormGroup;
  public administratorPharmaNetForm: FormGroup;
  public privacyOfficerForm: FormGroup;
  public technicalSupportForm: FormGroup;

  private siteId: number;
  private locationId: number;
  private organizationId: number;
  private provisionerId: number;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    // Initialize and configure the forms
    this.siteAddressForm = this.buildSiteAddressForm();
    this.hoursOperationForm = this.buildHoursOperationForm();
    this.vendorForm = this.buildVendorForm();
    this.administratorPharmaNetForm = this.buildAdministratorPharmaNetForm();
    this.privacyOfficerForm = this.buildPrivacyOfficerForm();
    this.technicalSupportForm = this.buildTechnicalSupportForm();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls.
   */
  public set site(site: Site) {
    // Store required site identifiers not captured in forms
    this.siteId = site.id;
    this.locationId = site.location.id;
    this.organizationId = site.location.organizationId;
    this.provisionerId = site.provisionerId;

    this.patchForm(site);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  // TODO method constructs the JSON, and attempts to adapt, should
  // adapt in only one place
  public get site(): Site {
    const physicalAddress = this.siteAddressForm.getRawValue();
    const businessHours = this.hoursOperationForm.getRawValue().businessDays;
    const vendor = this.vendorForm.getRawValue();

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
      return party;
    });

    // Includes site and location related keys to uphold relationships, and
    // allow for updates to a site. Keys not for update have been omitted
    // and the type enforced
    return {
      id: this.siteId,
      provisionerId: this.provisionerId,
      // provisioner
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
      // TODO pec not implemented
      // completed
      // approvedDate
      // submittedDate
    } as Site; // Enforced type
  }

  public get isValid() {
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
   * Helper for getting a list of organization forms.
   */
  private get forms(): AbstractControl[] {
    return [
      this.siteAddressForm,
      this.hoursOperationForm,
      this.vendorForm,
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
      ]
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
      id: [
        0,
        [Validators.required]
      ]
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
  private buildAddressForm(options: {
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
}
