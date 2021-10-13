import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';
import { ToggleContentChange } from '@shared/components/toggle-content/toggle-content.component';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaEnrollee } from '@phsa/shared/models/phsa-enrollee.model';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { PhsaEformsFormStateService } from '@phsa/shared/services/phsa-eforms-form-state.service';

@Component({
  selector: 'app-bcsc-demographic',
  templateUrl: './bcsc-demographic.component.html',
  styleUrls: ['./bcsc-demographic.component.scss']
})
export class BcscDemographicComponent implements OnInit {
  public enrollee: PhsaEnrollee;
  public form: FormGroup;
  public busy: Subscription;
  public hasVerifiedAddress: boolean;
  public hasMailingAddress: boolean;
  public hasPhysicalAddress: boolean;

  private routeUtils: RouteUtils;

  public constructor(
    protected fb: FormBuilder,
    protected route: ActivatedRoute,
    protected router: Router,
    private enrolmentFormStateService: PhsaEformsFormStateService,
    private utilService: UtilsService,
    private formUtilsService: FormUtilsService,
    private authService: AuthService
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaEformsRoutes.MODULE_PATH);
  }

  public get verifiedAddress(): FormGroup {
    return this.form.get('verifiedAddress') as FormGroup;
  }

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.routeUtils.routeRelativeTo(PhsaEformsRoutes.AVAILABLE_ACCESS);
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  public onPhysicalAddressChange({ checked }: ToggleContentChange): void {
    this.toggleAddressLineValidators(checked, this.physicalAddress);
  }

  public onMailingAddressChange({ checked }: ToggleContentChange): void {
    this.toggleAddressLineValidators(checked, this.mailingAddress, this.hasVerifiedAddress);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    // Ensure that the identity provider user information is loaded
    // prior to initialization of the form override form values, and
    // control the validation management
    this.getUser$()
      .pipe(
        map((enrollee: PhsaEnrollee) => this.enrollee = enrollee),
        map((_: PhsaEnrollee) => this.patchForm())
      )
      .subscribe(() => this.initForm());
  }

  private createFormInstance(): void {
    this.form = this.enrolmentFormStateService.demographicFormState.form;
  }

  private patchForm(): void {
    // Attempt to patch the form if not already patched
    this.enrolmentFormStateService.setForm(this.enrollee);
  }

  private initForm(): void {
    this.hasVerifiedAddress = Address.isNotEmpty(this.enrollee.verifiedAddress);
    if (!this.hasVerifiedAddress) {
      this.clearAddressValidator(this.verifiedAddress);
      this.setAddressValidator(this.physicalAddress);
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

  private getUser$(): Observable<PhsaEnrollee> {
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
            email: null,
            partyTypes: []
          } as PhsaEnrollee;
        })
      );
  }
}
