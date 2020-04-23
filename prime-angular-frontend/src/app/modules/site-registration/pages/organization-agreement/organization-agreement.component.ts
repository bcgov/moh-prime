import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

@Component({
  selector: 'app-organization-agreement',
  templateUrl: './organization-agreement.component.html',
  styleUrls: ['./organization-agreement.component.scss']
})
export class OrganizationAgreementComponent implements OnInit, IPage {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public organizationAgreement: string;
  public hasAcceptedAgreement: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  @ViewChild('accept') accepted: MatCheckbox;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    if (this.accepted.checked) {
      const siteId = this.siteRegistrationService.site.id;
      const data: DialogOptions = {
        title: 'Organization Agreement',
        message: 'Are you sure you want to accept the Organization Agreement?',
        actionText: 'Accept Agreement'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.siteRegistrationResource.acceptCurrentOrganizationAgreement(siteId)
              : EMPTY
          )
        )
        .subscribe(() => this.nextRoute());
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
  }

  public nextRoute() {
    this.routeUtils.routeRelativeTo(SiteRoutes.VENDOR);
  }

  public ngOnInit(): void {
    this.hasAcceptedAgreement = !!this.siteRegistrationService.site.location.organization.acceptedAgreementDate;
    this.siteRegistrationResource
      .getOrganizationAgreement()
      .subscribe((organizationAgreement: string) => this.organizationAgreement = organizationAgreement);
  }
}
