import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable, of } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { Address } from '@shared/models/address.model';
import { Contact } from '@registration/shared/models/contact.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-technical-support',
  templateUrl: './technical-support.component.html',
  styleUrls: ['./technical-support.component.scss']
})
export class TechnicalSupportComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  private site: Site;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private organizationResource: OrganizationResource,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteFormStateService.json;
      const organizationId = this.route.snapshot.params.oid;
      const site = this.siteService.site;

      this.busy = this.organizationResource
        .getOrganizationById(organizationId)
        .pipe(
          map((organization: Organization) => !!organization.acceptedAgreementDate),
          exhaustMap((hasSignedOrgAgreement: boolean) =>
            this.siteResource.updateSite(payload)
              .pipe(map(() => hasSignedOrgAgreement))
          ),
          exhaustMap((hasSignedOrgAgreement: boolean) =>
            (hasSignedOrgAgreement)
              ? this.siteResource.updateCompleted(site.id)
                .pipe(map(() => hasSignedOrgAgreement))
              : of(hasSignedOrgAgreement)
          )
        )
        .subscribe((hasSignedOrgAgreement: boolean) => {
          this.form.markAsPristine();
          this.nextRoute(organizationId, hasSignedOrgAgreement);
        });
    }
  }

  public onSelect(contact: Contact) {
    if (!contact.physicalAddress) {
      contact.physicalAddress = new Address();
    }
    this.form.patchValue(contact);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.PRIVACY_OFFICER);
  }

  public nextRoute(organizationId: number, hasSignedOrgAgreement: boolean) {
    if (!hasSignedOrgAgreement) {
      const siteId = this.route.snapshot.params.sid;
      // Provide site for redirection after accepting the organization agreement
      this.routeUtils.routeTo([SiteRoutes.routePath(SiteRoutes.SITE_MANAGEMENT), organizationId, SiteRoutes.ORGANIZATION_AGREEMENT], {
        queryParams: { redirect: `${SiteRoutes.SITES}/${siteId}`, siteId }
      });
    } else {
      this.routeUtils.routeRelativeTo(['../', SiteRoutes.SITE_REVIEW]);
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
    this.form = this.siteFormStateService.technicalSupportForm;
  }

  private initForm() {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.siteFormStateService.setForm(this.site, true);
    this.form.markAsPristine();
  }
}
