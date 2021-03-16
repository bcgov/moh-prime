import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Subscription, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Address, optionalAddressLineItems } from '@shared/models/address.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-organization-signing-authority-page',
  templateUrl: './organization-signing-authority-page.component.html',
  styleUrls: ['./organization-signing-authority-page.component.scss']
})
export class OrganizationSigningAuthorityPageComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog,
    private authService: AuthService
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
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

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get fax(): FormControl {
    return this.form.get('fax') as FormControl;
  }

  public get smsPhone(): FormControl {
    return this.form.get('smsPhone') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.organizationFormStateService.json;
      this.organizationResource.updateOrganization(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onPreferredNameChange({ checked }: MatSlideToggleChange): void {
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

  public onBack(): void {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public nextRoute(): void {
    const redirectPath = this.route.snapshot.queryParams.redirect;
    if (redirectPath) {
      this.routeUtils.routeRelativeTo([redirectPath, SiteRoutes.SITE_REVIEW]);
    } else {
      const routePath = (this.isCompleted)
        ? SiteRoutes.ORGANIZATION_REVIEW
        : SiteRoutes.ORGANIZATION_NAME;

      this.routeUtils.routeRelativeTo(routePath);
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
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
          this.form.patchValue({ firstName, lastName, givenNames, verifiedAddress });
        })
      )
      .subscribe(() => this.initForm());
  }

  private createFormInstance(): void {
    const formState = this.organizationFormStateService.organizationSigningAuthorityFormState;
    this.form = formState.form;
  }

  private patchForm(): void {
    // Store a local copy of the organization for views
    this.organization = this.organizationService.organization;
    this.isCompleted = this.organization?.completed;

    // Attempt to patch the form if not already patched
    this.organizationFormStateService.setForm(this.organization, true);
  }

  private initForm(): void {
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
