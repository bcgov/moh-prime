import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';
// TODO move to a bcsc module
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { SatEformsEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { SatEformsEnrolmentResource } from '@sat/shared/resource/sat-eforms-enrolment-resource.service';
import { DemographicFormState } from '@sat/pages/demographic-page/demographic-form-state.class';

// TODO create inheritable demographic class + mixins for reuse
@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: DemographicFormState;
  public enrollee: SatEformsEnrollee;
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
    private enrolmentResource: SatEformsEnrolmentResource,
    private authService: AuthService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
  }

  // public onSubmit() {
  //   this.routeUtils.routeRelativeTo(SatEformsRoutes.REGULATORY);
  // }

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

    // Ensure that the identity provider user information is loaded
    // prior to initialization of the form override form values, and
    // control the validation management
    this.authService.getUser$()
      .pipe(
        map((bcscUser: BcscUser) => this.bcscUser = bcscUser),
        // Patch the form using the stored enrolment information
        map((bcscUser: BcscUser) => {
          this.patchForm();
          return bcscUser;
        }),
        // BCSC information should always use identity provider profile
        // information as the source of truth, and patch the form to
        // have it save any changes
        map((bcscUser: BcscUser) => {
          const { firstName, lastName, givenNames } = bcscUser;
          const verifiedAddress = bcscUser.verifiedAddress ?? new Address();
          this.formState.form.patchValue({ firstName, lastName, givenNames, verifiedAddress });
        })
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

    // TODO provide through service initialized through guard
    this.enrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        if (enrollee) {
          this.enrollee = enrollee;

          // TODO patch the entire enrollee don't destructure
          const {
            firstName,
            givenNames,
            lastName,
            preferredFirstName,
            preferredMiddleName,
            preferredLastName,
            dateOfBirth,
            verifiedAddress,
            physicalAddress,
            email,
            phone
          } = enrollee;

          // Attempt to patch the form if not already patched
          this.formState.patchValue({
            firstName,
            givenNames,
            lastName,
            preferredFirstName,
            preferredMiddleName,
            preferredLastName,
            dateOfBirth,
            verifiedAddress,
            physicalAddress,
            email,
            phone
          });
        }
      });
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

  protected performSubmission(): NoContent {
    const enrolleeId = +this.route.snapshot.params.eid;
    return this.enrolmentResource.updateDemographic(enrolleeId, this.formState.json);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(SatEformsRoutes.REGULATORY);
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
}
