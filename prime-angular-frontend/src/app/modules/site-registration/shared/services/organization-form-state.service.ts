import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { Party } from '@registration/shared/models/party.model';
import { Organization } from '@registration/shared/models/organization.model';

// TODO add a form state service interface/abstract class for form state services
// TODO should the forms built be stored in a different file or service
@Injectable({
  providedIn: 'root'
})
export class OrganizationFormStateService {
  public signingAuthorityForm: FormGroup;
  public organizationInformationForm: FormGroup;
  public organizationTypeForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
  ) {
    // Initialize and configure the forms
    this.signingAuthorityForm = this.buildSigningAuthorityForm();
    this.organizationInformationForm = this.buildOrganizationInformationForm();
    this.organizationTypeForm = this.buildOrganizationTypeForm();
  }

  /**
   * @description
   * Convert JSON into reactive form abstract controls.
   */
  public set organization(organization: Organization) {
    this.patchForm(organization);
  }

  /**
   * @description
   * Convert reactive form abstract controls into JSON.
   */
  // TODO method constructs the JSON, and attempts to adapt, should
  // adapt in only one place
  public get organization(): Organization {
    const organizationInformation = this.organizationInformationForm.getRawValue();
    const { organizationTypeCode } = this.organizationTypeForm.getRawValue();

    // TODO duplicated until services are completely split apart
    const [
      signingAuthority
    ] = [
      this.signingAuthorityForm.getRawValue()
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
        const { physicalAddress, ...party } = data;

        formGroup.patchValue(party);

        const physicalAddressFormGroup = formGroup.get('physicalAddress');
        (physicalAddress)
          ? physicalAddressFormGroup.patchValue(physicalAddress)
          : physicalAddressFormGroup.reset();
      });
  }

  private buildSigningAuthorityForm(): FormGroup {
    // Prevent BCSC information from being changed
    return this.partyFormGroup(true);
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
}
