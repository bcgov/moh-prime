import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { exhaustMap, map, take, tap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrollee } from '@shared/models/enrollee.model';
import { Enrolment } from '@shared/models/enrolment.model';

import { User } from '@auth/shared/models/user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-demographic',
  templateUrl: './demographic.component.html',
  styleUrls: ['./demographic.component.scss']
})
export class DemographicComponent extends BaseEnrolmentProfilePage implements OnInit {
  /**
   * @description
   * Enrollee profile information not contained within the form.
   */
  public enrollee: Enrollee;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private authService: AuthService,
    private formUtilsService: FormUtilsService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentFormStateService, toastService, logger, utilService);
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
    this.getUser();
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.demographicForm;
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
  }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    if (!enrolment.id && this.isInitialEnrolment) {
      return this.authService.getUser$()
        .pipe(
          map(({ userId, hpdid, firstName, lastName, givenNames, dateOfBirth, physicalAddress }: User) => {
            // Enforced the enrolment type instead of using Partial<Enrolment>
            // to avoid creating constructors and partials for every model
            return {
              // Providing only the minimum required fields for creating an enrollee
              userId,
              hpdid,
              firstName,
              lastName,
              givenNames,
              dateOfBirth,
              physicalAddress,
              voicePhone: null,
              contactEmail: null
            } as Enrollee;
          }),
          exhaustMap((enrollee: Enrollee) => this.enrolmentResource.createEnrollee(enrollee)),
          map((newEnrolment: Enrolment) => {
            newEnrolment.enrollee = { ...newEnrolment.enrollee, ...enrolment.enrollee };
            return newEnrolment;
          }),
          // Populate generated keys within the form state (eg. id, userId, etc)
          tap((newEnrolment: Enrolment) => this.enrolmentFormStateService.setEnrolment(newEnrolment, true)),
          exhaustMap((newEnrolment: Enrolment) => super.performHttpRequest(newEnrolment))
        );
    } else {
      return super.performHttpRequest(enrolment, beenThroughTheWizard);
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

  private getUser(): void {
    this.authService.getUser$()
      .pipe(take(1))
      .subscribe(({ userId, hpdid, firstName, lastName, givenNames, dateOfBirth, physicalAddress }: User) => {
        // Enforced the enrollee type instead of using Partial<Enrollee>
        // to avoid creating constructors and partials for every model
        this.enrollee = {
          // Providing only the minimum required fields for creating an enrollee
          userId,
          hpdid,
          firstName,
          lastName,
          givenNames,
          dateOfBirth,
          physicalAddress,
          voicePhone: null,
          contactEmail: null
        } as Enrollee;
      });
  }
}
