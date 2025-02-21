import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from '@auth/shared/enum/role.enum';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { OrganizationAdminView } from '@registration/shared/models/organization.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-organization-table',
  templateUrl: './organization-table.component.html',
  styleUrls: ['./organization-table.component.scss']
})
export class OrganizationTableComponent implements OnInit, OnChanges {
  @Input() public organizations: OrganizationAdminView[];
  @Input() public localStoragePrefix: string;
  @Input() public hideOverviewButton: boolean;

  @Output() public refresh: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public delete: EventEmitter<{ [key: string]: number }>;
  @Output() public redirectSiteRegistration: EventEmitter<string | (string | number)[]>;

  public busy: Subscription;
  public dataSource: MatTableDataSource<OrganizationAdminView>;
  public columns: string[];
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;

  protected routeUtils: RouteUtils;

  constructor(
    private activatedRoute: ActivatedRoute,
    router: Router
  ) {
    this.refresh = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.delete = new EventEmitter<{ [key: string]: number }>();
    this.redirectSiteRegistration = new EventEmitter<string | (string | number)[]>();

    this.columns = [
      'prefixes',
      'displayId',
      'organizationName',
      'doingBusinessAs',
      'signingAuthority',
      'createdDate',
      'validSite',
      'actions'
    ];
    this.dataSource = new MatTableDataSource<OrganizationAdminView>([]);
    this.routeUtils = new RouteUtils(activatedRoute, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ORGANIZATIONS));
  }

  public ngOnChanges(changes: SimpleChanges): void {
    this.dataSource.data = this.organizations;
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.route.emit(routePath);
  }

  public onRedirectSiteRegistration(routePath: string | (string | number)[]): void {
    this.redirectSiteRegistration.emit(routePath);
  }

  public onReload(): void {
    this.refresh.emit();
  }

  public ngOnInit(): void {
    this.dataSource.data = this.organizations;
  }

  public onDelete(record: { [key: string]: number }) {
    this.delete.emit(record);
  }
}
