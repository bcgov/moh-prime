import { Component, OnInit, ViewChild } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EMPTY, noop, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { OrganizationAgreement, OrganizationAgreementViewModel } from '@shared/models/agreement.model';

@Component({
  selector: 'app-electronic-organization-agreement-page',
  templateUrl: './electronic-organization-agreement-page.component.html',
  styleUrls: ['./electronic-organization-agreement-page.component.scss']
})
export class ElectronicOrganizationAgreementPageComponent implements OnInit {
  public busy: Subscription;
  public organizationAgreement: OrganizationAgreementViewModel;
  public agreementId: number;

  public routeUtils: RouteUtils;

  @ViewChild('accept') public accepted: MatCheckbox;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    private router: Router,
    private organizationResource: OrganizationResource,
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_MANAGEMENT);
  }

  public onSubmit(): void {
    const organizationId = this.route.snapshot.params.oid;
    const data: DialogOptions = {
      title: 'Organization Agreement',
      message: 'Are you sure you want to accept the Organization Agreement?',
      actionText: 'Accept Organization Agreement'
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.organizationResource
              .acceptOrganizationAgreement(organizationId, this.agreementId)
            : EMPTY
        ),
      )
      .subscribe(() => {
        this.routeUtils.routeRelativeTo(['../'])
      });;
  }

  ngOnInit(): void {
    this.getAgreement();
  }

  private getAgreement(): void {
    const organizationId = this.route.snapshot.params.oid;
    const careSettingCode = this.route.snapshot.params.csid;

    this.busy = this.organizationResource
      .updateOrganizationAgreement(organizationId, careSettingCode)
      .pipe(
        map(({ id }: OrganizationAgreement) =>
          this.agreementId = id
        ),
        exhaustMap((agreementId: number) =>
          this.organizationResource.getOrganizationAgreement(organizationId, agreementId)
        )
      )
      .subscribe((organizationAgreement: OrganizationAgreementViewModel) =>
        this.organizationAgreement = organizationAgreement
      );
  }

}
