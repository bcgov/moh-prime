import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of, Subject } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { OrganizationAgreement } from '@shared/models/agreement.model';
import { Address } from '@shared/models/address.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { TechnicalSupportPageFormState } from './technical-support-page-form-state.class';

@Component({
  selector: 'app-technical-support-page',
  templateUrl: './technical-support-page.component.html',
  styleUrls: ['./technical-support-page.component.scss']
})
export class TechnicalSupportPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: TechnicalSupportPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  private site: Site;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private organizationResource: OrganizationResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSelect(contact: Contact) {
    if (!contact.physicalAddress) {
      contact.physicalAddress = new Address();
    }
    this.formState.form.patchValue(contact);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.PRIVACY_OFFICER);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.technicalSupportFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.siteFormStateService.setForm(this.site, true);
    this.formState.form.markAsPristine();
  }

  protected performSubmission(): Observable<boolean> {
    const payload = this.siteFormStateService.json;
    const organizationId = this.route.snapshot.params.oid;
    const site = this.siteService.site;

    return this.organizationResource
      .updateOrganizationAgreement(organizationId, site.careSettingCode)
      .pipe(
        map((agreement: OrganizationAgreement) => !!agreement),
        exhaustMap((needsOrgAgreement: boolean) =>
          this.siteResource.updateSite(payload)
            .pipe(map(() => needsOrgAgreement))
        ),
        exhaustMap((needsOrgAgreement: boolean) =>
          // Mark the site as completed if an organization
          // agreement does not need to be signed
          (!needsOrgAgreement)
            ? this.siteResource.setSiteCompleted(site.id)
              .pipe(map(() => needsOrgAgreement))
            : of(needsOrgAgreement)
        )
      );
  }

  protected afterSubmitIsSuccessful(needsOrgAgreement?: boolean): void {
    this.formState.form.markAsPristine();

    const routePath = (needsOrgAgreement)
      ? SiteRoutes.ORGANIZATION_AGREEMENT
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
