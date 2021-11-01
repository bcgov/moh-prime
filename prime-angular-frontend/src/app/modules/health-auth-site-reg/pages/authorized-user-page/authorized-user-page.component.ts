import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ToggleContentChange } from '@shared/components/toggle-content/toggle-content.component';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { AuthorizedUserFormState } from './authorized-user-form-state.class';

@Component({
  selector: 'app-authorized-user-page',
  templateUrl: './authorized-user-page.component.html',
  styleUrls: ['./authorized-user-page.component.scss']
})
export class AuthorizedUserPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: AuthorizedUserFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isApproved: boolean;
  /**
   * @description
   * User information from the provider not contained
   * within the form for use in creation.
   */
  public bcscUser: BcscUser;
  public hasPreferredName: boolean;
  public hasVerifiedAddress: boolean;
  public hasPhysicalAddress: boolean;
  public healthAuthorities: Config<number>[];

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private healthAuthorityResource: HealthAuthorityResource,
    private authService: AuthService,
    private configService: ConfigService,
    private authorizedUserService: AuthorizedUserService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.healthAuthorities = configService.healthAuthorities;
  }

  public onPreferredNameChange({ checked }: ToggleContentChange): void {
    if (!this.hasPreferredName) {
      this.formState.form.get('preferredMiddleName').reset();
    }

    this.togglePreferredNameValidators(checked, this.formState.preferredFirstName, this.formState.preferredLastName);
  }

  public onPhysicalAddressChange({ checked }: ToggleContentChange): void {
    this.toggleAddressLineValidators(checked, this.formState.physicalAddress);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.SITE_MANAGEMENT);
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

  protected createFormInstance(): void {
    this.formState = new AuthorizedUserFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(): void {
    // Store a local copy of the authorized user for views
    const authorizedUser = this.authorizedUserService.authorizedUser;
    this.isApproved = authorizedUser?.status === AccessStatusEnum.ACTIVE;

    // Attempt to patch the form
    this.formState.patchValue(authorizedUser);
  }

  protected initForm(): void {
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
    const authorizedUserId = this.authorizedUserService.authorizedUser?.id;
    const payload = this.formState.json;

    return (!authorizedUserId)
      ? this.healthAuthorityResource.createAuthorizedUser({ ...this.bcscUser, ...payload })
        .pipe(NoContentResponse)
      : this.healthAuthorityResource.updateAuthorizedUser({ ...payload, id: authorizedUserId });
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(HealthAuthSiteRegRoutes.ACCESS_REQUESTED);
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
