import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-organization-signing-authority',
  templateUrl: './organization-signing-authority.component.html',
  styleUrls: ['./organization-signing-authority.component.scss']
})
export class OrganizationSigningAuthorityComponent implements OnInit, IPage, IForm {
  // TODO Show 2 subheaders (normal one and information button one)
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public organization: Organization;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.title = 'Signing Authority';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get preferredFirstName(): FormControl {
    return this.form.get('preferredFirstName') as FormControl;
  }

  public get preferredLastName(): FormControl {
    return this.form.get('preferredLastName') as FormControl;
  }

  public get physicalAddress() {
    return (this.organization && this.organization.signingAuthority.physicalAddress)
      ? this.organization.signingAuthority.physicalAddress
      : null;
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
    // TODO structured to match in all organization views
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO when spoking don't update
      const payload = this.organizationFormStateService.organization;
      this.organizationResource
        .updateOrganization(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
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
    this.toggleMailingAddressValidators(this.mailingAddress, ['street2', 'id']);
  }

  public onBack() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS]);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_INFORMATION);
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
    // TODO structured to match in all site views
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization);

    this.organization = organization;

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

    this.toggleMailingAddressValidators(this.mailingAddress, ['street2', 'id']);
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
      // const id = mailingAddress.get('id') as FormControl;
      // this.formUtilsService.resetAndClearValidators(id);
      this.formUtilsService.setValidators(mailingAddress, [Validators.required], blacklist);
    }
  }
}
