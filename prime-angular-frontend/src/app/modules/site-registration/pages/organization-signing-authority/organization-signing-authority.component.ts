import { Component, OnInit } from '@angular/core';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { Subscription, Observable } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-organization-signing-authority',
  templateUrl: './organization-signing-authority.component.html',
  styleUrls: ['./organization-signing-authority.component.scss']
})
export class OrganizationSigningAuthorityComponent implements OnInit, IPage, IForm {

  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public site: Site;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get preferredFirstName(): FormControl {
    return this.form.get('preferredFirstName') as FormControl;
  }

  public get preferredLastName(): FormControl {
    return this.form.get('preferredLastName') as FormControl;
  }

  public get physicalAddress() {
    return (this.site && this.site.location.organization.signingAuthority.physicalAddress)
      ? this.site.location.organization.signingAuthority.physicalAddress
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

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteRegistrationStateService.site;
      this.siteRegistrationResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.HOURS_OPERATION);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.ADMINISTRATOR);
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.site = this.siteRegistrationService.site;
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.siteRegistrationStateService.signingAuthorityForm;
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site?.completed;
    this.siteRegistrationStateService.setSite(site, true);
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

}
