import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';

import { iif, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';
// TODO move to a bcsc module
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { SatEformsEnrolmentResource } from '@sat/shared/resource/sat-eforms-enrolment-resource.service';
import { SatEnrolleeService } from '@sat/shared/services/sat-enrollee.service';
import { DemographicFormState } from './demographic-form-state.class';

// TODO create inheritable demographic class + mixins for reuse
@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: DemographicFormState;
  public enrollee: SatEnrollee;
  public routeUtils: RouteUtils;
  /**
   * @description
   * User information from the provider not contained
   * within the form for use in creation.
   */
  public bcscUser: BcscUser;
  public hasPreferredName: boolean;
  public hasVerifiedAddress: boolean;
  public hasPhysicalAddress: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private location: Location,
    private enrolmentResource: SatEformsEnrolmentResource,
    private enrolleeService: SatEnrolleeService,
    private authService: AuthService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
  }

  public onPreferredNameChange({ checked }: { checked: boolean }): void {
    if (!this.hasPreferredName) {
      this.formState.form.get('preferredMiddleName').reset();
    }

    this.togglePreferredNameValidators(checked, this.formState.preferredFirstName, this.formState.preferredLastName);
  }

  public onPhysicalAddressChange({ checked }: { checked: boolean }): void {
    this.toggleAddressLineValidators(checked, this.formState.physicalAddress);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getUser$()
      .pipe(
        map((bcscUser: BcscUser) => this.bcscUser = bcscUser),
        map(_ => this.patchForm())
      )
      .subscribe(() => this.initForm());
  }

  protected createFormInstance() {
    this.formState = new DemographicFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      // Don't throw an error as new enrolments are created in this view
      return;
    }

    this.enrollee = this.enrolleeService.enrollee;
    this.formState.patchValue(this.enrollee);
  }

  protected initForm() {
    this.hasPreferredName = !!(this.formState.preferredFirstName.value || this.formState.preferredLastName.value);
    this.togglePreferredNameValidators(this.hasPreferredName, this.formState.preferredFirstName, this.formState.preferredLastName);

    this.hasVerifiedAddress = Address.isNotEmpty(this.bcscUser.verifiedAddress);
    if (!this.hasVerifiedAddress) {
      this.clearAddressValidator(this.formState.verifiedAddress);
      this.setAddressValidator(this.formState.physicalAddress);
    } else {
      this.hasPhysicalAddress = Address.isNotEmpty(this.formState.physicalAddress.value);
      this.toggleAddressLineValidators(this.hasPhysicalAddress, this.formState.physicalAddress);
    }
  }

  protected performSubmission(): Observable<number> {
    const demographic = this.formState.json;
    const enrolleeId = +this.route.snapshot.params.eid;

    return this.getUser$()
      .pipe(
        map((user: BcscUser) => {
          // Ensure BCSC user information is never overwritten
          const { firstName, lastName, givenNames, verifiedAddress, ...remainder } = user;
          return { ...remainder, ...demographic, firstName, lastName, givenNames, verifiedAddress };
        }),
        exhaustMap((enrollee: SatEnrollee) =>
          (!enrolleeId)
            ? this.enrolmentResource.createSatEnrollee(enrollee)
              .pipe(
                map((enrollee: SatEnrollee) => {
                  // Replace the URL with redirection, and prevent initial
                  // ID of zero being pushed onto browser history
                  this.location.replaceState([SatEformsRoutes.MODULE_PATH, enrollee.id, SatEformsRoutes.DEMOGRAPHIC].join('/'));
                  return enrollee.id;
                })
              )
            : this.enrolmentResource.updateSatEnrollee(enrolleeId, enrollee)
              .pipe(map(() => enrolleeId))
        )
      );
  }

  protected afterSubmitIsSuccessful(enrolleeId: number): void {
    // Must go up a route-level and down with newly minted or existing
    // enrollee ID to override the replaced route state during submission
    this.routeUtils.routeRelativeTo(['../', enrolleeId, SatEformsRoutes.REGULATORY]);
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

  /**
   * @description
   * Get an enrollee from a BCSC user with appropriate default
   * for properties that are not currently provided.
   */
  private getUser$(): Observable<BcscUser> {
    return this.authService.getUser$()
      .pipe(map((user: BcscUser) => ({ ...user, email: null })));
  }
}
