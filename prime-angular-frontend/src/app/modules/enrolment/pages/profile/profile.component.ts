import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import * as moment from 'moment';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public form: FormGroup;
  public maxBirthDate: moment.Moment;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public countries: Config<string>[];
  public provinces: Config<string>[];
  public subheadings: { [key: string]: { subheader: string, help: string } };

  private isNewEnrolment: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private authService: AuthService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService,
    private viewportService: ViewportService,
    private logger: LoggerService,
  ) {
    this.maxBirthDate = moment();
    this.countries = this.configService.countries;
    this.provinces = this.configService.provinces;
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

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;

      const request$ = (this.isNewEnrolment)
        ? this.enrolmentResource.createEnrolment(payload)
          .pipe(
            tap(() => this.isNewEnrolment = false),
            map((enrolment: Enrolment) => this.enrolmentStateService.enrolment = enrolment)
          )
        : this.enrolmentResource.updateEnrolment(payload);

      request$
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Profile information has been saved');
            this.form.markAsPristine();
            this.router.navigate(['contact'], { relativeTo: this.route.parent });
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
    const mailingAddress = this.form.get('mailingAddress') as FormGroup;

    this.toggleValidators(mailingAddress);
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

    this.enrolmentResource.enrolments()
      .subscribe(
        async (enrolment: Enrolment) => {
          if (enrolment) {
            this.isNewEnrolment = false;
            this.enrolmentStateService.enrolment = enrolment;
          } else {
            const user = await this.authService.getUser();

            this.logger.info('USER', user);

            this.form.patchValue(user);
            this.enrolmentStateService.contactForm.patchValue(user);
          }

          this.initForm();
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[Enrolment] Profile::getEnrolment error has occurred: ', error);
        }
      );
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

    const mailingAddress = this.form.get('mailingAddress') as FormGroup;

    // TODO: update to use valueChanges by forcing value changes when visible
    // Show mailing address if it exists
    this.hasMailingAddress = !!(
      mailingAddress.get('countryCode').value ||
      mailingAddress.get('provinceCode').value ||
      mailingAddress.get('street').value ||
      mailingAddress.get('city').value ||
      mailingAddress.get('postal').value
    );

    this.toggleValidators(mailingAddress);
  }

  private toggleValidators(mailingAddress: FormGroup) {
    if (!this.hasMailingAddress) {
      this.formUtilsService.resetAndClearValidators(mailingAddress);
    } else {
      this.formUtilsService.setValidators(mailingAddress, [Validators.required]);
    }
  }
}
