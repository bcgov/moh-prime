import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { FormBuilder, FormGroup, FormArray, Validators, FormControl } from '@angular/forms';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-signing-authority',
  templateUrl: './signing-authority.component.html',
  styleUrls: ['./signing-authority.component.scss']
})
export class SigningAuthorityComponent implements OnInit {
  form: FormGroup;
  public hasSeparateAddress: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private formUtilsService: FormUtilsService,
    private formBuilder: FormBuilder
  ) { }

  public get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  public get jobRole(): FormControl {
    return this.form.get('jobRole') as FormControl;
  }

  public get collegeName(): FormControl {
    return this.form.get('collegeName') as FormControl;
  }

  public get collegeId(): FormControl {
    return this.form.get('collegeId') as FormControl;
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

  public onSeparateAddressChange() {
    this.hasSeparateAddress = !this.hasSeparateAddress;
    this.toggleSeparateAddressValidators(this.separateAddress, ['street2']);
  }


  ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.formBuilder.group({
      name: [null, []],
      jobRole: [null, []],
      collegeName: [null, []],
      collegeId: [null, []],
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      fax: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      smsPhone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      email: [null, [Validators.required, FormControlValidators.email]],
      separateAddress: this.formBuilder.group({
        countryCode: [{ value: null, disabled: false }, []],
        provinceCode: [{ value: null, disabled: false }, []],
        street: [{ value: null, disabled: false }, []],
        street2: [{ value: null, disabled: false }, []],
        city: [{ value: null, disabled: false }, []],
        postal: [{ value: null, disabled: false }, []]
      }),
    });
  }

  protected initForm() {
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

  onSubmit() {

  }

  onBack() {
    this.router.navigate([SiteRoutes.VENDOR], { relativeTo: this.route.parent });
  }

  private toggleSeparateAddressValidators(separateAddress: FormGroup, blacklist: string[] = []) {
    if (!this.hasSeparateAddress) {
      this.formUtilsService.resetAndClearValidators(separateAddress);
    } else {
      this.formUtilsService.setValidators(separateAddress, [Validators.required], blacklist);
    }
  }

}
