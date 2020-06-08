import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';

import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';

// TODO default is null and on reset it would be great if no special 0 id was required
// TODO add a form state service interface/abstract class for form state services
// TODO should the forms built be stored in a different file or service
@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService {
  public signingAuthorityForm: FormGroup;
  public organizationInformationForm: FormGroup;
  public organizationTypeForm: FormGroup;

  private patched: boolean;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
  ) {
    // Initial state of the form is unpatched and ready for
    // enrolment information
    this.patched = false;

    this.init();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls, which can
   * only be set more than once when explicitly forced.
   */
  public setForm(organization: Organization, forcePatch: boolean = false) {
    if (this.patched && !forcePatch) {
      return;
    }

    // Indicate that the form is patched, and may contain unsaved information
    this.patched = true;

    this.patchForm(organization);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  // TODO method constructs the JSON, and attempts to adapt, should
  // adapt in only one place and separately in method
  public get organization(): Organization {
    const organizationInformation = this.organizationInformationForm.getRawValue();
    const { organizationTypeCode } = this.organizationTypeForm.getRawValue();

    // TODO duplicated until services are completely split apart
    const [
      signingAuthority
    ] = [
      this.signingAuthorityForm.getRawValue()
    ].map((party: Party) => {
      if (!party.firstName) {
        party = null;
      } else if (!party.mailingAddress.street) {
        party.mailingAddress = null;
      } else if (party.mailingAddress.street) {
        if (party.mailingAddress.id == null) {
          party.mailingAddress.id = 0;
        }
      }

      return party;
    });

    signingAuthority.mailingAddressId = signingAuthority.mailingAddress?.id
      ? signingAuthority.mailingAddress?.id
      : 0;

    return {
      // OrganizationInformation is the only form
      // that contains the organization ID
      ...organizationInformation,
      organizationTypeCode,
      signingAuthorityId: signingAuthority?.id,
      signingAuthority
    };
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
   * Initialize and configure the forms for patching, which is also used
   * clear previous form data from the service.
   */
  public init() {
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.organizationInformationForm = this.buildOrganizationInformationForm();
    this.organizationTypeForm = this.buildOrganizationTypeForm();
  }

  /**
   * @description
   * Helper for getting a list of organization forms.
   */
  private get forms(): AbstractControl[] {
    return [
      this.signingAuthorityForm,
      this.organizationInformationForm,
      this.organizationTypeForm
    ];
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  // TODO refactor into separate adapters
  private patchForm(organization: Organization): Organization {
    if (!organization) {
      return null;
    }

    this.organizationInformationForm.patchValue(organization);
    this.organizationTypeForm.patchValue(organization);

    // TODO duplicated until services are completely split apart
    [
      [this.signingAuthorityForm, organization.signingAuthority]
    ]
      .filter(([formGroup, data]: [FormGroup, Party]) => data)
      .forEach(([formGroup, data]: [FormGroup, Party]) => {
        const { mailingAddress, physicalAddress, ...party } = data;

        formGroup.patchValue(party);

        const physicalAddressFormGroup = formGroup.get('physicalAddress');
        (physicalAddress)
          ? physicalAddressFormGroup.patchValue(physicalAddress)
          : physicalAddressFormGroup.reset({ id: 0 });

        const mailingAddressFormGroup = formGroup.get('mailingAddress');
        (mailingAddress)
          ? mailingAddressFormGroup.patchValue(mailingAddress)
          : mailingAddressFormGroup.reset({ id: 0 });
      });
  }

  private buildSigningAuthorityForm(): FormGroup {
    // Prevent BCSC information from being changed
    return this.fb.group({
      id: [
        0,
        []
      ],
      firstName: [
        { value: null, disabled: true },
        [Validators.required]
      ],
      lastName: [
        { value: null, disabled: true },
        [Validators.required]
      ],
      preferredFirstName: [
        null, []
      ],
      preferredMiddleName: [
        null, []
      ],
      preferredLastName: [
        null, []
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      phone: [
        null,
        [FormControlValidators.phone]
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
      physicalAddress: this.fb.group({
        id: [
          0,
          []
        ],
        countryCode: [
          { value: null, disabled: true },
          []
        ],
        provinceCode: [
          { value: null, disabled: true },
          []
        ],
        street: [
          { value: null, disabled: true },
          []
        ],
        street2: [
          { value: null, disabled: true },
          []
        ],
        city: [
          { value: null, disabled: true },
          []
        ],
        postal: [
          { value: null, disabled: true },
          []
        ]
      }),
      mailingAddress: this.fb.group({
        id: [
          0,
          []
        ],
        countryCode: [
          { value: null, disabled: false },
          []
        ],
        provinceCode: [
          { value: null, disabled: false },
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
        postal: [
          { value: null, disabled: false },
          []
        ]
      }),
    });
  }

  private buildOrganizationInformationForm(): FormGroup {
    return this.fb.group({
      // OrganizationInformation is the only form
      // that contains the organization ID
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

  /**
   * @deprecated no longer used in organizations
   */
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
