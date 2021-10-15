import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { Enrollee } from '@shared/models/enrollee.model';
import { Address, AddressLine } from '@shared/models/address.model';

import { BceidUser } from '@auth/shared/models/bceid-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

import { BceidDemographicFormState } from './bceid-demographic-form-state.class';

@Component({
  selector: 'app-bceid-demographic',
  templateUrl: './bceid-demographic.component.html',
  styleUrls: ['./bceid-demographic.component.scss']
})
export class BceidDemographicComponent extends BaseEnrolmentProfilePage implements OnInit {
  public formState: BceidDemographicFormState;
  /**
   * @description
   * User information from the provider.
   */
  public user: BceidUser;
  public addressFormControlNames: AddressLine[];
  public maxDateOfBirth: moment.Moment;
  public hasMailingAddress: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService,
      authService
    );

    this.addressFormControlNames = [
      'street',
      'city',
      'provinceCode',
      'countryCode',
      'postal'
    ];
    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');
  }

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  public get dateOfBirth(): FormControl {
    return this.form.get('dateOfBirth') as FormControl;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  protected createFormInstance() {
    this.formState = this.enrolmentFormStateService.bceidDemographicFormState;
    this.form = this.formState.form;
  }

  protected initForm() {
    if (!this.enrolmentService.enrolment) {
      this.getUser$()
        .subscribe((enrollee: Enrollee) => {
          this.dateOfBirth.enable();
          this.form.patchValue(enrollee);
        });
    } else {
      this.hasMailingAddress = Address.isNotEmpty(this.mailingAddress.value);
    }
  }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    const enrollee = this.form.getRawValue();
    // BCeID has to match BCSC for submission, which requires givenNames
    const givenNames = enrollee.firstName;

    if (!enrolment.id && this.isInitialEnrolment) {
      const payload = {
        enrollee: { ...enrollee, givenNames },
        identificationDocumentGuid: this.enrolmentFormStateService.identityDocumentForm.get('identificationDocumentGuid').value
      };
      return this.enrolmentResource.createEnrollee(payload)
        .pipe(
          // Merge the enrolment with generated keys
          map((newEnrolment: Enrolment) => {
            newEnrolment.enrollee = { ...newEnrolment.enrollee, ...enrolment.enrollee };
            return newEnrolment;
          }),
          // Populate generated keys within the form state
          tap((newEnrolment: Enrolment) => this.enrolmentFormStateService.setForm(newEnrolment, true)),
          this.handleResponse()
        );
    } else {
      enrolment.enrollee.givenNames = givenNames;
      return super.performHttpRequest(enrolment, beenThroughTheWizard);
    }
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  protected getUser$(): Observable<Enrollee> {
    return this.authService.getUser$()
      .pipe(
        map(({ firstName, lastName, email = null }: BceidUser) => {
          // Enforced the enrollee type instead of using Partial<Enrollee>
          // to avoid creating constructors and partials for every model
          return {
            // Providing only the minimum required fields for creating an enrollee
            firstName,
            lastName,
            email
          } as Enrollee;
        })
      );
  }
}
