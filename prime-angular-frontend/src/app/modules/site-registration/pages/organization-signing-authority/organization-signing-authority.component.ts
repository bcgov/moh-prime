import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { IFormPage } from '@lib/classes/abstract-form-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { Address } from '@shared/models/address.model';

@Component({
  selector: 'app-organization-signing-authority',
  templateUrl: './organization-signing-authority.component.html',
  styleUrls: ['./organization-signing-authority.component.scss']
})
export class OrganizationSigningAuthorityComponent implements OnInit, IPage, IFormPage {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public organization: Organization;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public hasPhysicalAddress: boolean;

  private optionalAddressLineItems: string[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.optionalAddressLineItems = ['id', 'street2'];
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

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.organizationFormStateService.json;
      this.organizationResource
        .updateOrganization(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onPreferredNameChange({ checked }: MatSlideToggleChange) {
    if (!this.hasPreferredName) {
      this.form.get('preferredMiddleName').reset();
    }

    this.togglePreferredNameValidators(checked, this.preferredFirstName, this.preferredLastName);
  }

  public onMailingAddressChange({ checked }: MatSlideToggleChange) {
    this.toggleAddressLineValidators(checked, this.mailingAddress);
  }

  public onPhysicalAddressChange({ checked }: MatSlideToggleChange) {
    this.toggleAddressLineValidators(checked, this.physicalAddress);
  }

  public onBack() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public nextRoute() {
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

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.organizationFormStateService.signingAuthorityForm;
  }

  private initForm() {
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization, true);
    this.form.markAsPristine();

    this.organization = organization;

    this.hasPreferredName = !!(this.preferredFirstName.value || this.preferredLastName.value);
    this.togglePreferredNameValidators(this.hasPreferredName, this.preferredFirstName, this.preferredLastName);
    // this.hasPhysicalAddress = Address.isNotEmpty(this.physicalAddress.value);
    // this.toggleAddressLineValidators(this.hasPhysicalAddress, this.physicalAddress);
    this.hasMailingAddress = Address.isNotEmpty(this.mailingAddress.value)
    this.toggleAddressLineValidators(this.hasMailingAddress, this.mailingAddress);
  }

  private togglePreferredNameValidators(hasPreferredName: boolean, preferredFirstName: FormControl, preferredLastName: FormControl) {
    if (!hasPreferredName) {
      this.formUtilsService.resetAndClearValidators(preferredFirstName);
      this.formUtilsService.resetAndClearValidators(preferredLastName);
    } else {
      this.formUtilsService.setValidators(preferredFirstName, [Validators.required]);
      this.formUtilsService.setValidators(preferredLastName, [Validators.required]);
    }
  }

  private toggleAddressLineValidators(hasAddressLine: boolean, addressLine: FormGroup): void {
    (!hasAddressLine)
      ? this.formUtilsService.resetAndClearValidators(addressLine, this.optionalAddressLineItems)
      : this.formUtilsService.setValidators(addressLine, [Validators.required], this.optionalAddressLineItems);
  }
}
