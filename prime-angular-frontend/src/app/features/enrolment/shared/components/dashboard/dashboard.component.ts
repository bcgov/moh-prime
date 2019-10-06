import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

import { ViewportService } from '@core/services/viewport.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidenav', { static: false }) public sideNav: MatSidenav;

  public sideNavSections: {};
  public sideNavProps: {
    mode: string,
    opened: boolean,
    fixedInViewport: boolean,
    showText: boolean
  };

  constructor(
    private viewportService: ViewportService,
    private router: Router
  ) { }

  /**
   * Check viewport size is equivalent to desktop.
   *
   * @returns {boolean}
   * @memberof DashboardComponent
   */
  public get isDesktop(): boolean {
    return this.viewportService.isDesktop || this.viewportService.isWideDesktop;
  }

  /**
   * Route to the next view.
   *
   * @param {string} route
   * @memberof DashboardComponent
   */
  public routeTo(route: string) {
    if (this.viewportService.isMobile) {
      this.sideNav.close();
    }

    this.router.navigate([route]);
  }

  /**
   * Handle on route event.
   *
   * @memberof DashboardComponent
   */
  public onRoute(): void {
    if (this.viewportService.isMobile) {
      this.sideNav.close();
    }
  }

  /**
   * Logout the authenticated user.
   *
   * @memberof DashboardComponent
   */
  // public logout() {
  //   this.authResource.logout()
  //     .subscribe(
  //       () => { this.logger.error('Logout response should never be 200.'); },
  //       (error) => {
  //         this.routeMessengerService.set(error.message);
  //         // Indicate the route is associated to logging out for
  //         // deactivation guards
  //         const navigationExtras: NavigationExtras = {
  //           queryParams: { logout: true }
  //         };
  //         this.router.navigate([this.config.routes.auth], navigationExtras);
  //       }
  //     );
  // }

  public ngOnInit() {
    this.sideNavSections = this.getSideNavSections();
    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);
    // Subscribe to viewport onresize changes
    this.viewportService.onResize()
      .subscribe((device: string) => this.setSideNavProps(device));
  }

  /**
   * Get the side navigation sections.
   *
   * @private
   * @returns
   * @memberof DashboardComponent
   */
  private getSideNavSections() {
    return this.getEnroleeSideNavSections();
  }

  /**
   * Get the sidenav sections.
   *
   * @private
   * @returns
   * @memberof DashboardComponent
   */
  private getEnroleeSideNavSections() {
    return [
      {
        header: 'Application Enrolment',
        showHeader: true,
        items: [
          {
            name: 'Enrollee Information',
            icon: 'person',
            route: '/enrolment/profile',
            showItem: true
          },
          {
            name: 'Contact Information',
            icon: 'phone',
            route: '/enrolment/contact',
            showItem: true
          },
          {
            name: 'Professional Information',
            icon: 'work',
            route: '/enrolment/professional',
            showItem: true
          },
          {
            name: 'Self Declaration',
            icon: 'description',
            route: '/enrolment/declaration',
            showItem: true
          },
          {
            name: 'PharmaNet Access',
            icon: 'location_city',
            route: '/enrolment/access',
            showItem: true
          },
          {
            name: 'Review',
            icon: 'search',
            route: '/enrolment/review',
            showItem: true
          }
        ]
      },
      {
        header: '',
        showHeader: false,
        items: [
          {
            name: '',
            icon: '',
            route: '',
            showItem: false
          },
          {
            name: '',
            icon: '',
            route: '',
            showItem: false
          }
        ]
      }
    ];
  }

  /**
   * Set the properties of the side navigation.
   *
   * @private
   * @param {string} device
   * @memberof DashboardComponent
   */
  private setSideNavProps(device: string) {
    if (device === DeviceResolution.MOBILE) {
      this.sideNavProps = {
        mode: 'over',
        opened: false,
        fixedInViewport: false,
        showText: false
      };
    } else if (device === DeviceResolution.TABLET) {
      this.sideNavProps = {
        mode: 'side',
        opened: true,
        fixedInViewport: false,
        showText: false
      };
    } else {
      this.sideNavProps = {
        mode: 'side',
        opened: true,
        fixedInViewport: false,
        showText: true
      };
    }
  }
}
