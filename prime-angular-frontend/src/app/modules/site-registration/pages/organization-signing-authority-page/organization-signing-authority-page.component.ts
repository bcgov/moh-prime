import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Party } from '@lib/models/party.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';
import { ToggleContentChange } from '@shared/components/toggle-content/toggle-content.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationSigningAuthorityPageFormState } from './organization-signing-authority-page-form-state.class';

@Component({
  selector: 'app-organization-signing-authority-page',
  templateUrl: './organization-signing-authority-page.component.html',
  styleUrls: ['./organization-signing-authority-page.component.scss']
})
export class OrganizationSigningAuthorityPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OrganizationSigningAuthorityPageFormState;
  public title: string;
  public organizationId: number;
  public organization: Organization;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
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
  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.organizationId = +this.route.snapshot.params.oid;
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

  public onMailingAddressChange({ checked }: ToggleContentChange): void {
    this.toggleAddressLineValidators(checked, this.formState.mailingAddress, this.hasVerifiedAddress);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_REVIEW);
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
    this.formState = this.organizationFormStateService.organizationSigningAuthorityPageFormState;
  }

  protected patchForm(): void {
    // Store a local copy of the organization for views
    this.organization = this.organizationService.organization;
    this.isCompleted = this.organization?.completed;

    // Attempt to patch the form if not already patched
    this.organizationFormStateService.setForm(this.organization, true);
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

    this.hasMailingAddress = Address.isNotEmpty(this.formState.mailingAddress.value);
    this.toggleAddressLineValidators(this.hasMailingAddress, this.formState.mailingAddress, this.hasVerifiedAddress);
  }

  protected performSubmission(): Observable<Party> {
    const payload = this.formState.json;
    return this.organizationResource.createSigningAuthority(payload);
  }

  protected afterSubmitIsSuccessful(party: Party): void {
    const redirectPath = this.route.snapshot.queryParams.redirect;
    let routePath: (string | number)[];
    const organization = this.organizationService.organization;

    if (redirectPath) {
      routePath = [redirectPath, SiteRoutes.SITE_REVIEW];
    } else {
      routePath = (this.isCompleted)
        ? ['../', organization.id, SiteRoutes.ORGANIZATION_REVIEW]
        : organization
          ? ['../', organization.id, SiteRoutes.ORGANIZATION_NAME]
          : ['../', 0, SiteRoutes.ORGANIZATION_CLAIM];
    }
    this.routeUtils.routeRelativeTo(routePath);
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
