import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Observable } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

import { BcscDemographicFormState } from './bcsc-demographic-form-state.class';

@Component({
  selector: 'app-bcsc-demographic',
  templateUrl: './bcsc-demographic.component.html',
  styleUrls: ['./bcsc-demographic.component.scss']
})
export class BcscDemographicComponent extends BaseEnrolmentProfilePage implements OnInit {
  public formState: BcscDemographicFormState;
  /**
   * @description
   * User information from the provider not contained
   * within the form for use in creation.
   */
  public bcscUser: BcscUser;
  public hasPreferredName: boolean;
  public hasVerifiedAddress: boolean;
  public hasMailingAddress: boolean;
  public hasPhysicalAddress: boolean;
  public potentialPaperEnrolleeReturnee: boolean;

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
  }

  public get preferredFirstName(): FormControl {
    return this.form.get('preferredFirstName') as FormControl;
  }

  public get preferredLastName(): FormControl {
    return this.form.get('preferredLastName') as FormControl;
  }

  public get verifiedAddress(): FormGroup {
    return this.form.get('verifiedAddress') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  public onPreferredNameChange({ checked }: MatSlideToggleChange) {
    if (!this.hasPreferredName) {
      this.form.get('preferredMiddleName').reset();
    }

    this.togglePreferredNameValidators(checked, this.preferredFirstName, this.preferredLastName);
  }

  public onPhysicalAddressChange({ checked }: MatSlideToggleChange): void {
    this.toggleAddressLineValidators(checked, this.physicalAddress);
  }

  public onMailingAddressChange({ checked }: MatSlideToggleChange): void {
    this.toggleAddressLineValidators(checked, this.mailingAddress, this.hasVerifiedAddress);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.isPotentialPaperEnrolleeReturnee();
    this.patchForm()
      .pipe(
        map(([bcscUser, enrolment]: [BcscUser, Enrolment]) => {
          this.bcscUser = bcscUser;
          if (!enrolment) {
            // Manage patching the form state for a new enrolment
            // that has not been created
            const { firstName, lastName, givenNames } = bcscUser;
            const verifiedAddress = bcscUser.verifiedAddress ?? new Address();
            this.form.patchValue({ firstName, lastName, givenNames, verifiedAddress });
          }
        })
      )
      .subscribe(() => this.initForm());
  }

  protected createFormInstance(): void {
    this.formState = this.enrolmentFormStateService.bcscDemographicFormState;
    this.form = this.formState.form;
  }

  protected initForm(): void {
    this.hasPreferredName = !!(this.preferredFirstName.value || this.preferredLastName.value);
    this.togglePreferredNameValidators(this.hasPreferredName, this.preferredFirstName, this.preferredLastName);

    this.hasVerifiedAddress = Address.isNotEmpty(this.bcscUser.verifiedAddress);
    if (!this.hasVerifiedAddress) {
      this.clearAddressValidator(this.verifiedAddress);
      this.setAddressValidator(this.physicalAddress);
    } else {
      this.hasPhysicalAddress = Address.isNotEmpty(this.physicalAddress.value);
      this.toggleAddressLineValidators(this.hasPhysicalAddress, this.physicalAddress);
    }

    this.hasMailingAddress = Address.isNotEmpty(this.mailingAddress.value);
    this.toggleAddressLineValidators(this.hasMailingAddress, this.mailingAddress, this.hasVerifiedAddress);
  }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    if (!enrolment.id && this.isInitialEnrolment) {
      return this.getUser$()
        .pipe(
          map((enrollee: Enrollee) => {
            const { firstName, lastName, givenNames, verifiedAddress, ...remainder } = enrollee;
            const { userId, ...demographic } = enrolment.enrollee;
            return { ...remainder, ...demographic, firstName, lastName, givenNames, verifiedAddress };
          }),
          exhaustMap((enrollee: Enrollee) => this.enrolmentResource.createEnrollee({ enrollee })),
          // Populate the new enrolment within the form state by force patching
          tap((newEnrolment: Enrolment) => this.enrolmentFormStateService.setForm(newEnrolment, true)),
          this.handleResponse()
        )
    } else {
      return super.performHttpRequest(enrolment, beenThroughTheWizard);
    }
  }

  protected nextRouteAfterSubmit(): void {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private togglePreferredNameValidators(hasPreferredName: boolean, preferredFirstName: FormControl, preferredLastName: FormControl): void {
    if (!hasPreferredName) {
      this.formUtilsService.resetAndClearValidators(preferredFirstName);
      this.formUtilsService.resetAndClearValidators(preferredLastName);
    } else {
      this.formUtilsService.setValidators(preferredFirstName, [Validators.required]);
      this.formUtilsService.setValidators(preferredLastName, [Validators.required]);
    }
  }

  private toggleAddressLineValidators(hasAddressLine: boolean, addressLine: FormGroup, shouldToggle: boolean = true): void {
    if (!shouldToggle) {
      return;
    }

    (!hasAddressLine)
      ? this.clearAddressValidator(addressLine)
      : this.setAddressValidator(addressLine);
  }

  private clearAddressValidator(addressLine: FormGroup): void {
    this.formUtilsService.resetAndClearValidators(addressLine, optionalAddressLineItems);
  }

  private setAddressValidator(addressLine: FormGroup): void {
    this.formUtilsService.setValidators(addressLine, [Validators.required], optionalAddressLineItems);
  }

  private isPotentialPaperEnrolleeReturnee(): void {
    this.getUser$()
      .subscribe(enrollee => {
        this.enrolmentResource.getPotentialPaperEnrolleeReturneeStatus(enrollee.dateOfBirth)
          .subscribe((result: boolean) => this.potentialPaperEnrolleeReturnee = result);
      })
  }

  private getUser$(): Observable<Enrollee> {
    return this.authService.getUser$()
      .pipe(
        map(({ userId, hpdid, firstName, lastName, givenNames, dateOfBirth, verifiedAddress }: BcscUser) => {
          // Enforced the enrollee type instead of using Partial<Enrollee>
          // to avoid creating constructors and partials for every model
          return {
            // Providing only the minimum required fields for creating an enrollee
            userId,
            hpdid,
            firstName,
            lastName,
            givenNames,
            dateOfBirth,
            verifiedAddress,
            phone: null,
            email: null
          } as Enrollee;
        })
      );
  }
}
