import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterEvent } from '@angular/router';

import { Observable, of } from 'rxjs';

import { RouteStateService } from '@core/services/route-state.service';
import { DashboardNavSection } from '@shared/models/dashboard.model';
import { AuthRoutes } from '@auth/auth.routes';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-registration-dashboard',
  templateUrl: './site-registration-dashboard.component.html',
  styleUrls: ['./site-registration-dashboard.component.scss']
})
export class SiteRegistrationDashboardComponent implements OnInit {
  public dashboardNavSections: Observable<DashboardNavSection[]>;
  public logoutRedirectUrl: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private routeStateService: RouteStateService,
  ) {
    this.logoutRedirectUrl = AuthRoutes.routePath(AuthRoutes.SITE);
  }

  public ngOnInit(): void {
    this.dashboardNavSections = this.getDashboardNavSections();

    this.routeStateService
      .onNavigationEnd()
      .subscribe((routerEvent: RouterEvent) => {
        // TODO check route event for destination and mark as active
        console.log('ROUTE_EVENT', routerEvent);
        console.log('ROUTE', this.route);
        console.log('ROUTE_SNAPSHOT', this.route.snapshot);
        console.log('PARAMS', this.route.snapshot.params);
        console.log('QUERY_PARAMS', this.route.snapshot.queryParams);
        console.log('QUERY_PARAMS_MAP', this.route.snapshot.queryParamMap);
      });
  }

  private getDashboardNavSections(): Observable<DashboardNavSection[]> {
    // TODO show nothing if on site management
    // TODO show organizations and sites
    // TODO show organizations
    // TODO show sites

    return of([
      {
        items: [
          {
            name: 'Site Management',
            icon: 'store',
            route: SiteRoutes.SITE_MANAGEMENT
          }
        ]
      },
      {
        items: [
          {
            name: 'Organization Information',
            route: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY
          },
          {
            name: 'Signing Authority',
            icon: 'store',
            route: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY
          },
          {
            name: 'Organization Name',
            icon: 'store',
            route: SiteRoutes.ORGANIZATION_NAME
          }
        ]
      },
      {
        items: [
          {
            name: 'Site Information',
            route: SiteRoutes.CARE_SETTING
          },
          {
            name: 'Care Setting',
            icon: 'store',
            route: SiteRoutes.CARE_SETTING
          },
          {
            name: 'Business Licence',
            icon: 'store',
            route: SiteRoutes.BUSINESS_LICENCE
          },
          {
            name: 'Site Address',
            icon: 'store',
            route: SiteRoutes.SITE_ADDRESS
          },
          {
            name: 'Hours of Operation',
            icon: 'store',
            route: SiteRoutes.HOURS_OPERATION
          },
          {
            name: 'PharmaNet Administrator',
            icon: 'store',
            route: SiteRoutes.ADMINISTRATOR
          },
          {
            name: 'Privacy Officer',
            icon: 'store',
            route: SiteRoutes.PRIVACY_OFFICER
          },
          {
            name: 'Technical Support Contact',
            icon: 'store',
            route: SiteRoutes.TECHNICAL_SUPPORT
          }
        ]
      },
      {
        items: [
          {
            name: 'Remote Practitioners',
            icon: 'store',
            route: SiteRoutes.REMOTE_USERS
          }
        ]
      },
      {
        items: [
          {
            name: 'Organization Agreement',
            icon: 'store',
            route: SiteRoutes.ORGANIZATION_AGREEMENT
          }
        ]
      }
    ]);
  }
}
