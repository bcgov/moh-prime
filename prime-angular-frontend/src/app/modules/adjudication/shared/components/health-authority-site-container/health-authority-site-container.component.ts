import { Component, Inject, Input, OnInit, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { EMPTY, noop, Observable, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { OrganizationResource } from '@core/resources/organization-resource.service';

@Component({
  selector: 'app-health-authority-site-container',
  templateUrl: './health-authority-site-container.component.html',
  styleUrls: ['./health-authority-site-container.component.scss']
})
export class HealthAuthoritySiteContainerComponent extends AbstractSiteAdminPage implements OnInit {
  @Input() public busy: Subscription;
  @Input() public content: TemplateRef<any>;
  @Input() public actions: TemplateRef<any>;
  @Input() public belowActions: TemplateRef<any>;
  @Input() public hasActions: boolean;
  @Input() public refresh: Observable<boolean>;

  public healthAuthoritySite: HealthAuthoritySiteAdminList;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthSiteResource: HealthAuthoritySiteResource,
    protected organizationResource: OrganizationResource,
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    private permissionService: PermissionService
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthSiteResource, organizationResource);

    this.hasActions = false;
  }

  public deleteSite(record: { siteId: number }) {
    if (record.siteId) {
      const request$ = this.siteResource.deleteSite(record.siteId);
      this.busy = this.deleteResource<HealthAuthoritySiteAdminList>(this.defaultOptions.delete('site'), request$)
        .subscribe(() => { });
    }
  }

  private deleteResource<T>(dialogOptions: DialogOptions, deleteRequest$: NoContent): Observable<T> {
    if (this.permissionService.hasRoles(Role.SUPER_ADMIN)) {
      return this.dialog.open(ConfirmDialogComponent, { data: dialogOptions })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? of(noop)
              : EMPTY
          ),
          exhaustMap(() => deleteRequest$),
          exhaustMap(() => {
            this.routeUtils.routeTo(
              [AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.SITE_REGISTRATIONS],
              { queryParams: { careSetting: CareSettingEnum.HEALTH_AUTHORITY } }
            );
            return EMPTY;
          })
        );
    }
  }

  public ngOnInit(): void {
    this.getDataset();

    this.refresh?.subscribe((shouldRefresh: boolean) => {
      if (shouldRefresh) {
        this.onRefresh();
      }
    });
  }

  protected getDataset(): void {
    this.busy = this.healthAuthSiteResource
      .getHealthAuthorityAdminSites(+this.route.snapshot.params.haid, +this.route.snapshot.params.sid)
      .subscribe((sites: HealthAuthoritySiteAdminList[]) => this.healthAuthoritySite = sites[0]);
  }

  protected updateSite(siteId: number, updatedSiteFields: {}): void {
    const updateHASite = {
      ...this.healthAuthoritySite,
      ...updatedSiteFields
    } as HealthAuthoritySiteAdminList;
    this.healthAuthoritySite = updateHASite;
  }
}
