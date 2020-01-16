import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { map, tap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';
import { Enrollee } from '@shared/models/enrollee.model';

@Component({
  selector: 'app-demographic',
  templateUrl: './demographic.component.html',
  styleUrls: ['./demographic.component.scss']
})
export class DemographicComponent extends BaseEnrolmentProfilePage implements OnInit {
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public isNewProfile: boolean;
  public enrollee: Partial<Enrollee>;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    private authService: AuthService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private enrolmentStateService: EnrolmentStateService,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    super(route, router, dialog);

    this.isNewProfile = false;
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

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      const request$ = (this.isNewProfile)
        ? this.enrolmentResource.createEnrollee(payload)
          .pipe(
            tap(() => this.isNewProfile = false),
            map((enrolment: Enrolment) => this.enrolmentStateService.enrolment = enrolment)
          )
        : this.enrolmentResource.updateEnrollee(payload);

      this.busy = request$
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Profile information has been saved');
            this.form.markAsPristine();
            const routePath = (!this.isProfileComplete)
              ? EnrolmentRoutes.REGULATORY
              : EnrolmentRoutes.OVERVIEW;
            this.routeTo(routePath);
          },
          (error: any) => {
            this.toastService.openErrorToast('Profile information could not be saved');
            this.logger.error('[Enrolment] Profile::onSubmit error has occurred: ', error);
          }
        );
    } else {
      this.form.markAllAsTouched();
    }
  }

  public onPreferredNameChange() {
    this.hasPreferredName = !this.hasPreferredName;

    if (!this.hasPreferredName) {
      this.form.get('preferredFirstName').reset();
      this.form.get('preferredMiddleName').reset();
      this.form.get('preferredLastName').reset();
    }
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
    this.form = this.enrolmentStateService.profileForm;
  }

  protected initForm() {
    // Show preferred name if it exists
    this.hasPreferredName = !!(
      this.form.get('preferredFirstName').value ||
      this.form.get('preferredMiddleName').value ||
      this.form.get('preferredLastName').value
    );

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

  protected async patchForm() {
    const enrolment = this.enrolmentService.enrolment;

    if (enrolment) {
      if (enrolment.enrollee) {
        this.enrollee = enrolment.enrollee;
      }

      this.enrolmentStateService.enrolment = enrolment;
      this.isInitialEnrolment = enrolment.progressStatus !== ProgressStatus.FINISHED;
      this.isProfileComplete = enrolment.profileCompleted;
    } else {
      const {
        firstName,
        lastName,
        dateOfBirth,
        physicalAddress
      } = await this.authService.getUser();

      this.enrollee = {
        firstName,
        lastName,
        dateOfBirth,
        physicalAddress
      };

      this.isNewProfile = true;
      this.isInitialEnrolment = true;
      this.isProfileComplete = false;

      this.patchFormWithUser();
    }
  }

  private async patchFormWithUser() {
    if (this.isNewProfile) {
      const user = await this.authService.getUser();

      this.logger.info('USER', user);

      this.form.patchValue(user);
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
