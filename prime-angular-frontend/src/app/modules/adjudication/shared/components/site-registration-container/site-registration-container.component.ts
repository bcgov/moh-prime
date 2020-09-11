import { Component, OnInit, Input, Output, TemplateRef, EventEmitter, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable, EMPTY, of, noop, combineLatest, concat } from 'rxjs';
import { exhaustMap, map, tap, take } from 'rxjs/operators';

import { MatTableDataSourceUtils } from '@lib/modules/ngx-material/mat-table-data-source-utils.class';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ClaimSiteComponent, ClaimSiteAction } from '@shared/components/dialogs/content/claim-site/claim-site.component';
import { ClaimActionEnum } from '@shared/components/dialogs/content/claim-enrollee/claim-enrollee.component';

import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Organization, OrganizationListViewModel } from '@registration/shared/models/organization.model';
import { Site, SiteListViewModel } from '@registration/shared/models/site.model';
import { SiteRegistrationListViewModel, SiteListViewModelPartial } from '@registration/shared/models/site-registration.model';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';

@Component({
  selector: 'app-site-registration-container',
  templateUrl: './site-registration-container.component.html',
  styleUrls: ['./site-registration-container.component.scss']
})
export class SiteRegistrationContainerComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public actions: TemplateRef<any>;
  @Input() public content: TemplateRef<any>;
  @Input() public refresh: Observable<boolean>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<SiteRegistrationListViewModel>;

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
    this.dataSource = new MatTableDataSource<SiteRegistrationListViewModel>([]);
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

  public onClaim(siteId: number) {
    this.siteResource
      .setSiteAdjudicator(siteId)
      .subscribe((updatedSite: Site) => this.updateSite(updatedSite));
  }

  public onDisclaim(siteId: number) {
    const data: DialogOptions = {
      title: 'Disclaim Site'
    };

    this.busy = this.dialog.open(ClaimSiteComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: ClaimSiteAction }) => {
          if (!result) { return EMPTY; }

          if (result.output.action === ClaimActionEnum.Disclaim) {
            return this.siteResource.removeSiteAdjudicator(siteId);
          } else if (result.output.action === ClaimActionEnum.Claim) {
            return concat(
              this.siteResource.removeSiteAdjudicator(siteId),
              this.siteResource.setSiteAdjudicator(siteId, result.output.adjudicatorId)
            );
          }
        })
      )
      .subscribe((updatedSite: Site) => this.updateSite(updatedSite));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public onDelete(record: { [key: string]: number }) {
    (record.organizationId)
      ? this.deleteOrganization(record.organizationId)
      : this.deleteSite(record.siteId);
  }

  public onApprove(siteId: number) {
    const data: DialogOptions = {
      title: 'Approve Site Registration',
      message: 'Are you sure you want to approve this Registration?',
      actionText: 'Approve Site Registration',
      component: NoteComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: string }) => (result) ? of(result.output ?? null) : EMPTY),
        exhaustMap((note: string) => this.siteResource.approveSite(siteId).pipe(map(() => note))),
        exhaustMap((note: string) =>
          (note)
            ? this.siteResource.createSiteRegistrationNote(siteId, note)
            : of(noop)
        ),
      )
      .subscribe();
  }

  public onDecline(siteId: number) {
    const data: DialogOptions = {
      title: 'Decline Site Registration',
      message: 'Are you sure you want to Decline this Site Registration?',
      actionText: 'Decline Site Registration',
      component: NoteComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: string }) => (result) ? of(result.output ?? null) : EMPTY),
        // TODO: Implement Decline pathway
        // exhaustMap((note: string) => this.siteResource.declineSite(siteId).pipe(map(() => note))),,
        exhaustMap((note: string) =>
          (note)
            ? this.siteResource.createSiteRegistrationNote(siteId, note)
            : of(noop)
        ),
      )
      .subscribe();
  }

  public ngOnInit(): void {
    // Use existing query params for initial search, and
    // update results on query param change
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
    const { oid, sid } = this.route.snapshot.params;
    const request$ = (oid)
      ? combineLatest([
        this.getOrganizationById(oid),
        this.getSiteById(sid)
      ])
        .pipe(
          take(1),
          map(this.toSiteRegistration())
        )
      : this.getOrganizations(queryParams)
        .pipe(
          map(this.toSiteRegistrations)
        );

    this.busy = request$
      .subscribe((siteRegistrations: SiteRegistrationListViewModel[]) => this.dataSource.data = siteRegistrations);
  }

  private getOrganizations({ search, status }: { search?: string, status?: number }): Observable<OrganizationListViewModel[]> {
    return this.organizationResource.getOrganizations()
      .pipe(
        tap(() => this.showSearchFilter = true)
      );
  }

  private getOrganizationById(organizationId: number): Observable<Organization> {
    return this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        map((organization: Organization) => organization),
        tap(() => this.showSearchFilter = false)
      );
  }

  private getSiteById(siteId: number): Observable<Site> {
    return this.siteResource.getSiteById(siteId);
  }

  private updateSite(updatedSite: Site) {
    const siteRegistration = this.dataSource.data.find((siteReg: SiteRegistrationListViewModel) => siteReg.siteId === updatedSite.id);
    const updatedSiteRegistration = {
      ...siteRegistration,
      ...this.toSiteViewModelPartial(updatedSite)
    };
    this.dataSource.data = MatTableDataSourceUtils
      .update<SiteRegistrationListViewModel>(this.dataSource, 'siteId', updatedSiteRegistration);
  }

  private deleteOrganization(organizationId: number) {
    if (organizationId) {
      const request$ = this.organizationResource.deleteOrganization(organizationId);
      this.busy = this.deleteResource<Organization>(this.defaultOptions.delete('organization'), request$)
        .subscribe((organization: Organization) =>
          this.dataSource.data = MatTableDataSourceUtils
            .delete<SiteRegistrationListViewModel>(this.dataSource, 'organizationId', organization.id)
        );
    }
  }

  private deleteSite(siteId: number) {
    if (siteId) {
      const request$ = this.siteResource.deleteSite(siteId);
      this.busy = this.deleteResource<Site>(this.defaultOptions.delete('site'), request$)
        .subscribe((site: Site) => {
          this.dataSource.data = MatTableDataSourceUtils
            .delete<SiteRegistrationListViewModel>(this.dataSource, 'siteId', site.id);
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

  private toSiteRegistrations(organizations: OrganizationListViewModel[]): SiteRegistrationListViewModel[] {
    const siteRegistrations = organizations.reduce((registrations, ovm) => {
      const { id: organizationId, sites, ...organization } = ovm;
      const registration = sites.map((svm: SiteListViewModel, index: number) => {
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

  private toSiteRegistration(): ([organization, site]: [Organization, Site]) => SiteRegistrationListViewModel[] {
    return ([organization, site]: [Organization, Site]) => {
      const {
        id: organizationId,
        displayId,
        signingAuthorityId,
        signingAuthority,
        name,
        signedAgreementDocuments,
        acceptedAgreementDate
      } = organization;

      return [{
        organizationId,
        displayId,
        signingAuthorityId,
        signingAuthority,
        name,
        signedAgreementDocumentCount: signedAgreementDocuments.length,
        acceptedAgreementDate,
        ...this.toSiteViewModelPartial(site)
      }];
    };
  }

  private toSiteViewModelPartial(site: Site): SiteListViewModelPartial {
    const {
      id: siteId,
      physicalAddress,
      doingBusinessAs,
      submittedDate,
      careSettingCode,
      siteVendors,
      adjudicator,
      pec
    } = site;

    return {
      siteId,
      physicalAddress,
      doingBusinessAs,
      submittedDate,
      careSettingCode,
      siteVendors,
      adjudicatorIdir: adjudicator?.idir,
      pec
    };
  }
}
