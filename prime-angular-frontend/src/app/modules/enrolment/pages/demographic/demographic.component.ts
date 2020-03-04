import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { UtilsService } from '@core/services/utils.service';

@Component({
  selector: 'app-demographic',
  templateUrl: './demographic.component.html',
  styleUrls: ['./demographic.component.scss']
})
export class DemographicComponent extends BaseEnrolmentProfilePage implements OnInit {
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private formUtilsService: FormUtilsService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger, utilService);
  }

  public get preferredFirstName(): FormControl {
    return this.form.get('preferredFirstName') as FormControl;
  }

  public get preferredLastName(): FormControl {
    return this.form.get('preferredLastName') as FormControl;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  public get voicePhone(): FormControl {
    return this.form.get('voicePhone') as FormControl;
  }

  public get voiceExtension(): FormControl {
    return this.form.get('voiceExtension') as FormControl;
  }

  public get hasContactEmail(): FormControl {
    return this.form.get('hasContactEmail') as FormControl;
  }

  public get contactEmail(): FormControl {
    return this.form.get('contactEmail') as FormControl;
  }

  public get hasContactPhone(): FormControl {
    return this.form.get('hasContactPhone') as FormControl;
  }

  public get contactPhone(): FormControl {
    return this.form.get('contactPhone') as FormControl;
  }

  public onPreferredNameChange() {
    this.hasPreferredName = !this.hasPreferredName;

    if (!this.hasPreferredName) {
      this.form.get('preferredMiddleName').reset();
    }

    this.togglePreferredNameValidators(this.preferredFirstName, this.preferredLastName);
  }

  public onMailingAddressChange() {
    this.hasMailingAddress = !this.hasMailingAddress;
    this.toggleMailingAddressValidators(this.mailingAddress, ['street2']);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.demographicForm;
  }

  protected initForm() {
    // Show preferred name if it exists
    this.hasPreferredName = !!(
      this.form.get('preferredFirstName').value ||
      this.form.get('preferredMiddleName').value ||
      this.form.get('preferredLastName').value
    );

    this.togglePreferredNameValidators(this.preferredFirstName, this.preferredLastName);

    // Show mailing address if it exists
    this.hasMailingAddress = !!(
      this.mailingAddress.get('countryCode').value ||
      this.mailingAddress.get('provinceCode').value ||
      this.mailingAddress.get('street').value ||
      this.mailingAddress.get('street2').value ||
      this.mailingAddress.get('city').value ||
      this.mailingAddress.get('postal').value
    );

    this.toggleMailingAddressValidators(this.mailingAddress, ['street2']);

    this.hasContactEmail.valueChanges
      .subscribe((value: boolean) =>
        this.toggleContactValidators(value, this.contactEmail)
      );

    this.hasContactPhone.valueChanges
      .subscribe((value: boolean) =>
        this.toggleContactValidators(value, this.contactPhone)
      );

    if (this.contactEmail.value) {
      this.form.get('hasContactEmail').patchValue(true);
    }

    if (this.contactPhone.value) {
      this.form.get('hasContactPhone').patchValue(true);
    }
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.REGULATORY;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private togglePreferredNameValidators(preferredFirstName: FormControl, preferredLastName: FormControl) {
    if (!this.hasPreferredName) {
      this.formUtilsService.resetAndClearValidators(preferredFirstName);
      this.formUtilsService.resetAndClearValidators(preferredLastName);
    } else {
      this.formUtilsService.setValidators(preferredFirstName, [Validators.required]);
      this.formUtilsService.setValidators(preferredLastName, [Validators.required]);
    }
  }

  private toggleMailingAddressValidators(mailingAddress: FormGroup, blacklist: string[] = []) {
    if (!this.hasMailingAddress) {
      this.formUtilsService.resetAndClearValidators(mailingAddress);
    } else {
      this.formUtilsService.setValidators(mailingAddress, [Validators.required], blacklist);
    }
  }

  private toggleContactValidators(value: boolean, control: FormControl) {
    if (!value) {
      this.formUtilsService.resetAndClearValidators(control);
    } else {
      this.formUtilsService.setValidators(control, [Validators.required]);
    }
  }
}
