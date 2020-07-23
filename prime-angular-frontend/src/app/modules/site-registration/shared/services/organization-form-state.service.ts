import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { FormControlValidators } from '@lib/validators/form-control.validators';

import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';
import { AbstractFormState } from '@registration/shared/classes/abstract-form-state.class';

@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService extends AbstractFormState<Organization> {
  public signingAuthorityForm: FormGroup;
  public organizationNameForm: FormGroup;

  constructor(
    protected fb: FormBuilder
  ) {
    super(fb);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  public get json(): Organization {
    const organizationName = this.organizationNameForm.getRawValue();

    // TODO create a helper to reconstruct the party into JSON format
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
      // OrganizationName is the only form that contains the organization ID
      ...organizationName,
      signingAuthorityId: signingAuthority?.id,
      signingAuthority
    };
  }

  /**
   * @description
   * List of constituent model forms, which is used at minimum to
   * drive internal form helper methods.
   */
  public get forms(): AbstractControl[] {
    return [
      this.signingAuthorityForm,
      this.organizationNameForm
    ];
  }

  /**
   * @description
   * Initialize and configure the forms for patching, which is also used
   * to clear previous form data from the service.
   */
  public init() {
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.organizationNameForm = this.buildOrganizationNameForm();
  }

  /**
   * @description
   * Manage the conversion of JSON to reactive forms.
   */
  // TODO refactor into separate adapters
  protected patchForm(organization: Organization): Organization {
    if (!organization) {
      return null;
    }

    this.organizationNameForm.patchValue(organization);

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

  private buildOrganizationNameForm(): FormGroup {
    return this.fb.group({
      // OrganizationName is the only form
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
}
