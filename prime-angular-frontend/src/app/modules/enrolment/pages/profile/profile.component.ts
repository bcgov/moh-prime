import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import * as moment from 'moment';

import { Observable, Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { ViewportService } from '@core/services/viewport.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public maxBirthDate: moment.Moment;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public subheadings: { [key: string]: { subheader: string, help: string } };
  public profileCompleted: boolean;
  public EnrolmentRoutes = EnrolmentRoutes;

  private isNewEnrolment: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private authService: AuthService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService,
    private viewportService: ViewportService,
    private logger: LoggerService,
  ) {
    this.maxBirthDate = moment();
    this.isNewEnrolment = true;
  }

  public get firstName(): FormControl {
    return this.form.get('firstName') as FormControl;
  }

  public get lastName(): FormControl {
    return this.form.get('lastName') as FormControl;
  }

  public get dateOfBirth(): FormControl {
    return this.form.get('dateOfBirth') as FormControl;
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

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      const request$ = (this.isNewEnrolment)
        ? this.enrolmentResource.createEnrollee(payload)
          .pipe(
            tap(() => this.isNewEnrolment = false),
            map((enrolment: Enrolment) => this.enrolmentStateService.enrolment = enrolment)
          )
        : this.enrolmentResource.updateEnrollee(payload);

      this.busy = request$
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Profile information has been saved');
            this.form.markAsPristine();
            this.router.navigate([EnrolmentRoutes.REGULATORY], { relativeTo: this.route.parent });
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
    this.toggleValidators(this.mailingAddress, ['street2']);
  }

  public onRoute(routePath: EnrolmentRoutes) {
    this.router.navigate([routePath], { relativeTo: this.route.parent });
  }

  public isRequired(path: string) {
    this.formUtilsService.isRequired(this.form, path);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();

    const enrolment = this.enrolmentService.enrolment;
    this.profileCompleted = (enrolment) ? enrolment.profileCompleted : true;

    if (enrolment) {
      this.isNewEnrolment = false;
      this.enrolmentStateService.enrolment = enrolment;
    }

    // TODO is this still needed?
    this.form.markAsPristine();

    this.initForm();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.profileForm;
  }

  private initForm() {
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

    this.toggleValidators(this.mailingAddress, ['street2']);

    this.hasContactEmail.valueChanges.subscribe((value: boolean) => this.toggleContactValidators(value, this.contactEmail));
    this.hasContactPhone.valueChanges.subscribe((value: boolean) => this.toggleContactValidators(value, this.contactPhone));

    if (this.contactEmail.value) {
      this.form.get('hasContactEmail').patchValue(true);
    }

    if (this.contactPhone.value) {
      this.form.get('hasContactPhone').patchValue(true);
    }

    this.patchForm();
  }

  private async patchForm() {
    if (this.isNewEnrolment) {
      const user = await this.authService.getUser();

      this.logger.info('USER', user);

      this.form.patchValue(user);
    }
  }

  private toggleValidators(mailingAddress: FormGroup, blacklist: string[] = []) {
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
