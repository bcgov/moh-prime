import { Component, OnInit, Input, Output, TemplateRef, EventEmitter, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';

import { AuthService } from '@auth/shared/services/auth.service';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Organization } from '@registration/shared/models/organization.model';
import { Site } from '@registration/shared/models/site.model';
import { Location } from '@registration/shared/models/location.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-site-registration-container',
  templateUrl: './site-registration-container.component.html',
  styleUrls: ['./site-registration-container.component.scss']
})
export class SiteRegistrationContainerComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public content: TemplateRef<any>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<Site>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    // private adjudicationResource: AdjudicationResource,
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.MODULE_PATH);

    this.action = new EventEmitter<void>();

    this.hasActions = false;
    this.dataSource = new MatTableDataSource<Site>([]);
  }

  public onSearch(search: string | null): void {
    this.routeUtils.updateQueryParams({ search });
  }

  public onFilter(status: any | null): void {
    this.routeUtils.updateQueryParams({ status });
  }

  public onRefresh(): void {
    // Use existing query params for initial search
    this.getDataset(this.route.snapshot.queryParams);

    // Update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => this.getDataset(queryParams));
  }

  // TODO
  public onDelete(siteId: number) {
    const data: DialogOptions = {
      ...this.defaultOptions.delete('site'),
      // TODO temporary until they decide whether notes are added to site registration
      // component: NoteComponent
    };

    if (this.authService.isSuperAdmin()) {
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          // TODO temporary until they decide whether notes are added to site registration
          // exhaustMap((result: { output: string }) => {
          //   if (result) {
          //     return (result.output)
          //       ? this.adjudicationResource.createAdjudicatorNote(siteId, result.output)
          //       : of(noop);
          //   }
          //   return EMPTY;
          // }),
          exhaustMap(() => this.siteResource.deleteSite(siteId))
        )
        .subscribe(() => this.routeUtils.routeRelativeTo(AdjudicationRoutes.SITE_REGISTRATIONS));
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit(): void {
    this.getDataset(this.route.snapshot.queryParams);
  }

  // private getDataset(queryParams: { search?: string, status?: number }): void {
  //   const organizationId = this.route.snapshot.params.id;
  //   const results$ = (organizationId)
  //     ? this.getOrganizationById(organizationId)
  //     : this.getOrganizations(queryParams);

  //   this.busy = results$
  //     .pipe(
  //       map((organizations: Organization[]) => {
  //         organizations.map((organization: Organization) => {

  //         });
  //       })
  //     )
  //     .subscribe((organizations: Organization[]) => this.dataSource.data = organizations);
  // }

  // private getOrganizations({ search, status }: { search?: string, status?: number }): Observable<Organization[]> {
  //   return this.organizationResource.getOrganizations();
  // }

  // private getOrganizationById(organizationId: number): Observable<Organization[]> {
  //   return this.organizationResource
  //     .getOrganizationById(organizationId)
  //     .pipe(
  //       map((organization: Organization) => [organization])
  //     );
  // }

  private getDataset(queryParams: { search?: string, status?: number }): void {
    const organizationId = this.route.snapshot.params.id;
    const results$ = (organizationId)
      ? this.getSiteById(organizationId)
      : this.getSites(queryParams);

    this.busy = results$
      .subscribe((sites: Site[]) => this.dataSource.data = sites);
  }

  private getSites({ search, status }: { search?: string, status?: number }): Observable<Site[]> {
    return this.siteResource.getAllSites();
  }

  private getSiteById(siteId: number): Observable<Site[]> {
    return this.siteResource
      .getSiteById(siteId)
      .pipe(
        map((site: Site) => [site])
      );
  }
}
