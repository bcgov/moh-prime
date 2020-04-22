import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { FormControlValidators } from '@shared/validators/form-control.validators';
import { FormUtilsService } from '@common/services/form-utils.service';

@Component({
  selector: 'app-registrant-profile-form',
  templateUrl: './registrant-profile-form.component.html',
  styleUrls: ['./registrant-profile-form.component.scss']
})
export class RegistrantProfileFormComponent implements OnInit {
  @Input() public title: string;
  public form: FormGroup;
  public submit: EventEmitter<{ [key: string]: any }>;
  public hasSeparateAddress: boolean;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    this.submit = new EventEmitter<{ [key: string]: any }>();
  }

  public get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  public get jobRole(): FormControl {
    return this.form.get('jobRole') as FormControl;
  }

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get fax(): FormControl {
    return this.form.get('fax') as FormControl;
  }

  public get smsPhone(): FormControl {
    return this.form.get('smsPhone') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public get separateAddress(): FormGroup {
    return this.form.get('separateAddress') as FormGroup;
  }

  public onSubmit() {
    // TODO proper submission when backend payload known
    // if (this.form.valid) { }
    this.submit.emit(this.form.value);
  }

  public onSeparateAddressChange() {
    this.hasSeparateAddress = !this.hasSeparateAddress;
    this.toggleSeparateAddressValidators(this.separateAddress, ['street2']);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    // TODO proper naming when backend payload known
    this.form = this.fb.group({
      firstName: [
        null,
        [Validators.required]
      ],
      lastName: [
        null,
        [Validators.required]
      ],
      jobRoleTitle: [
        null,
        [Validators.required]
      ],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      phone: [
        null, [
          Validators.required,
          FormControlValidators.phone
        ]
      ],
      fax: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      smsPhone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      separateAddress: this.fb.group({
        countryCode: [{ value: null, disabled: false }, []],
        provinceCode: [{ value: null, disabled: false }, []],
        street: [{ value: null, disabled: false }, []],
        street2: [{ value: null, disabled: false }, []],
        city: [{ value: null, disabled: false }, []],
        postal: [{ value: null, disabled: false }, []]
      })
    });
  }

  private initForm() {
    // Show separate address if it exists
    this.hasSeparateAddress = !!(
      this.separateAddress.get('countryCode').value ||
      this.separateAddress.get('provinceCode').value ||
      this.separateAddress.get('street').value ||
      this.separateAddress.get('street2').value ||
      this.separateAddress.get('city').value ||
      this.separateAddress.get('postal').value
    );

    this.toggleSeparateAddressValidators(this.separateAddress, ['street2']);
  }

  private toggleSeparateAddressValidators(separateAddress: FormGroup, blacklist: string[] = []) {
    if (!this.hasSeparateAddress) {
      this.formUtilsService.resetAndClearValidators(separateAddress);
    } else {
      this.formUtilsService.setValidators(separateAddress, [Validators.required], blacklist);
    }
  }
}
