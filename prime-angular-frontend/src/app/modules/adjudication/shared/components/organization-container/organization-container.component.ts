import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Component, Inject, Input, OnInit, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { OrganizationAdminView } from '@registration/shared/models/organization.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { EMPTY, exhaustMap, noop, of, Subscription } from 'rxjs';
import { SearchFormStatusType } from '@adjudication/shared/enums/search-form-status-type.enum';

@Component({
  selector: 'app-organization-container',
  templateUrl: './organization-container.component.html',
  styleUrls: ['./organization-container.component.scss']
})
export class OrganizationContainerComponent implements OnInit {
  @Input() public content: TemplateRef<any>;
  @Input() public hideSearchBar: boolean;
  @Input() public hideOverviewButton: boolean;

  public busy: Subscription;
  public organizations: OrganizationAdminView[];
  public AdjudicationRoutes = AdjudicationRoutes;
  public SearchFormStatusType = SearchFormStatusType;

  protected routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected adjudicationResource: AdjudicationResource,
    protected siteResource: SiteResource,
    protected organizationResource: OrganizationResource,
    private permissionService: PermissionService,
    @Inject(DIALOG_DEFAULT_OPTION) private defaultOptions: DialogDefaultOptions,
  ) {
    this.organizations = [];
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ORGANIZATIONS));
  }

  public onSearch(textSearch: string | null): void {
    this.routeUtils.updateQueryParams({ textSearch, page: null });
  }

  public onRefresh(): void {
    this.getDataset(this.route.snapshot.params.id, this.route.snapshot.queryParams);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public onRedirectToSiteRegistration(routePath: string | (string | number)[]) {
    this.router.navigate([AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.SITE_REGISTRATIONS, ...routePath]);
  }

  public onDelete(organizationId: number) {
    this.deleteOrganization(organizationId);
  }

  public ngOnInit(): void {
    // Use existing query params for initial search, and
    // update results on query param change
    this.route.queryParams
      .subscribe((queryParams: { [key: string]: any }) => {
        // Search is not applicable to single-row enrollee
        if (!this.route.snapshot.params.id) {
          this.getDataset(this.route.snapshot.params.id, queryParams);
        }
      });
    // url params could change due to jump action, subscribe to changes
    this.route.params
      .subscribe((params) => {
        if (params.id) {
          this.getDataset(params.id, {});
        }
      });
  }

  protected getDataset(
    organizationId: number,
    queryParams: {
      textSearch?: string,
    }
  ) {
    if (organizationId) {
      this.getOrganizationById(organizationId);
    } else {
      this.getOrganizations(queryParams.textSearch);
    }
  }

  private getOrganizationById(organizationId: number) {
    this.busy = this.adjudicationResource.getOrganizationById(organizationId)
      .subscribe((organization: OrganizationAdminView) => {
        this.organizations = [organization];
      });
  }

  private getOrganizations(textSearch: string) {
    this.busy = this.adjudicationResource.getOrganizations(textSearch)
      .subscribe((organizations: OrganizationAdminView[]) => {
        this.organizations = organizations;
      });
  }

  private deleteOrganization(organizationId: number) {
    if (organizationId && this.permissionService.hasRoles(Role.SUPER_ADMIN)) {
      const supplementaryMessage = 'Deleting an organization also deletes all the organization\'s sites, including remote users.';

      this.busy = this.dialog.open(ConfirmDialogComponent, { data: this.defaultOptions.delete('organization', supplementaryMessage) })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? of(noop)
              : EMPTY
          ),
          exhaustMap(() => this.organizationResource.deleteOrganization(organizationId)),
          exhaustMap(() => {
            this.organizations = this.organizations.filter((o) => o.id !== organizationId);
            this.routeUtils.routeTo([AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.ORGANIZATIONS]);
            return EMPTY;
          })
        ).subscribe();
    }
  }
}
