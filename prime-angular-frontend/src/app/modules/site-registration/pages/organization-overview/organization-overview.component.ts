import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-organization-overview',
  templateUrl: './organization-overview.component.html',
  styleUrls: ['./organization-overview.component.scss']
})
export class OrganizationOverviewComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public routeUtils: RouteUtils;
  public organization: Organization;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private dialog: MatDialog
  ) {
    this.title = 'Organization Information Review';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    if (this.organization.acceptedAgreementDate) {
      return this.nextRoute();
    }

    // TODO shouldn't come from service when spoking to save updates
    // const payload = this.organizationService.organization;
    const data: DialogOptions = {
      title: 'Save Organization',
      message: 'When your organization is saved you will be able to register site(s). Are you ready to save your organization?',
      actionText: 'Save Organization'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        // TODO not needed until updates are allowed
        // TODO update only required when spoking
        // exhaustMap((result: boolean) =>
        //   (result)
        //     ? this.organizationResource.submitOrganization(payload)
        //     : EMPTY
        // )
      )
      .subscribe(() => this.nextRoute());
  }

  public onBack() {
    if (!this.organization.acceptedAgreementDate) {
      this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_TYPE);
    } else {
      this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS]);
    }
  }

  public nextRoute() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS]);
  }

  public onRoute(routePath: string) {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit(): void {
    this.organization = this.organizationService.organization;
  }
}
