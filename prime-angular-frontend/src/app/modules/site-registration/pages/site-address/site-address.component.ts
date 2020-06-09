import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationResource } from '@registration/shared/services/organization-resource.service';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';

// TODO rename to SiteLocationComponent
// TODO rename form to siteLocationForm in SiteFormStateService
@Component({
  selector: 'app-site-address',
  templateUrl: './site-address.component.html',
  styleUrls: ['./site-address.component.scss']
})
export class SiteAddressComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public formControlNames: string[];
  public locationNames: { group: string, options: string[] }[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationResource: OrganizationResource,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private orgBookResource: OrgBookResource,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.title = 'Location Name';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];

    this.locationNames = [];
  }

  public get name(): FormGroup {
    return this.form.get('name') as FormGroup;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public onSubmit() {
    // TODO structured to match in all site views
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO when spoking don't update
      const payload = this.siteFormStateService.site;
      this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onBack() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS]);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.BUSINESS_LICENCE);
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
    this.form = this.siteFormStateService.siteAddressForm;
  }

  private initForm() {
    // TODO structured to match in all site views
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // TODO cannot set form each time the view is loaded when updating
    this.siteFormStateService.setForm(site, true);

    this.busy = this.organizationResource.getOrganizationById(site.location.organizationId)
      .pipe(
        map((organization: Organization) => {
          this.locationNames.push({
            group: 'Organization',
            options: [organization.name]
          });

          return organization.registrationId;
        }),
        this.orgBookResource.doingBusinessAsMap(),
        map((doingBusinessAs: string[]) => {
          this.locationNames.push({
            group: 'Doing Business As',
            options: doingBusinessAs
          });
        })
      )
      .subscribe();
  }
}
