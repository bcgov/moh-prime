import { Component, OnInit, Input, Output, TemplateRef, EventEmitter, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable, EMPTY, of, noop } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { AuthService } from '@auth/shared/services/auth.service';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Site, SiteViewModel } from '@registration/shared/models/site.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Organization, OrganizationViewModel } from '@registration/shared/models/organization.model';
import { SiteRegistrationViewModel } from '@adjudication/shared/models/site-registration.model';

@Component({
  selector: 'app-site-registration-container',
  templateUrl: './site-registration-container.component.html',
  styleUrls: ['./site-registration-container.component.scss']
})
export class SiteRegistrationContainerComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public content: TemplateRef<any>;
  @Input() public refresh: Observable<boolean>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<OrganizationViewModel>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));

    this.action = new EventEmitter<void>();

    this.hasActions = false;
    this.dataSource = new MatTableDataSource<Organization>([]);
  }

  public onSearch(search: string | null): void {
    this.routeUtils.updateQueryParams({ search });
  }

  public onFilter(status: any | null): void {
    this.routeUtils.updateQueryParams({ status });
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.queryParams);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public onDelete(record: { [key: string]: number }) {
    (record.organizationId)
      ? this.deleteOrganization(record?.organizationId)
      : this.deleteSite(record?.siteId);
  }

  public ngOnInit(): void {
    // Use existing query params for initial search
    this.getDataset(this.route.snapshot.queryParams);

    // Update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => this.getDataset(queryParams));

    // Listen for requests to refresh the data layer
    if (this.refresh instanceof Observable) {
      this.refresh.subscribe((shouldRefresh: boolean) => {
        if (shouldRefresh) {
          this.onRefresh();
        }
      });
    }
  }

  private getDataset(queryParams: { search?: string, status?: number }): void {
    const organizationId = this.route.snapshot.params.oid;
    const results$ = (organizationId)
      ? this.getOrganizationById(organizationId)
      : this.getOrganizations(queryParams);

    this.busy = results$
      .pipe(map(this.toSiteRegistrations))
      .subscribe((siteRegistrations: SiteRegistrationViewModel[]) => this.dataSource.data = siteRegistrations);
  }

  private getOrganizations({ search, status }: { search?: string, status?: number }): Observable<Organization[]> {
    return this.organizationResource.getOrganizations();
  }

  private getOrganizationById(organizationId: number): Observable<Organization[]> {
    return this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        map((organization: Organization) => [organization])
      );
  }

  private toSiteRegistrations(organizations: OrganizationViewModel[]): SiteRegistrationViewModel[] {
    const siteRegistrations = organizations.reduce((registrations, ovm) => {
      const { id: organizationId, sites, ...organization } = ovm;
      const registration = sites.map((svm: SiteViewModel, index: number) => {
        const { id: siteId, ...site } = svm;
        return (!index)
          ? { organizationId, ...organization, siteId, ...site }
          : { organizationId, siteId, ...site };
      });
      registrations.push(registration);
      return registrations;
    }, []);

    return [].concat(...siteRegistrations);
  }

  private deleteOrganization(organizationId: number) {
    if (organizationId) {
      const request$ = this.organizationResource.deleteOrganization(organizationId);
      this.busy = this.deleteResource<Organization>(this.defaultOptions.delete('organization'), request$)
        .subscribe((organization: Organization) =>
          this.dataSource.data = MatTableDataSourceUtils
            .deleteById<OrganizationViewModel>(this.dataSource, organization.id)
        );
    }
  }

  private deleteSite(siteId: number) {
    if (siteId) {
      const request$ = this.siteResource.deleteSite(siteId);
      this.busy = this.deleteResource<Site>(this.defaultOptions.delete('site'), request$)
        .subscribe((site: Site) => {
          this.dataSource.data =
            MatTableDataSourceUtils
              .deleteRelatedById<OrganizationViewModel, Site>(this.dataSource, site.organizationId, 'sites', site.id);
        });
    }
  }

  private deleteResource<T>(dialogOptions: DialogOptions, deleteRequest$: Observable<T>): Observable<T> {
    if (this.authService.isSuperAdmin()) {
      return this.dialog.open(ConfirmDialogComponent, { data: dialogOptions })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? of(noop)
              : EMPTY
          ),
          exhaustMap(() => deleteRequest$),
          exhaustMap((resource: T) => {
            // Route on singular resource views after deletion to refresh results
            if (this.route.snapshot.data.oid) {
              this.routeUtils.routeTo(AdjudicationRoutes.SITE_REGISTRATIONS);
              return EMPTY;
            }
            // Otherwise, stay on the list resource view and remove locally
            return of(resource);
          })
        );
    }
  }
}
