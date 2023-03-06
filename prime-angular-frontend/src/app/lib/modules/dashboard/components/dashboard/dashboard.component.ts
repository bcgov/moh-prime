import { Component, OnInit, ViewChild, Inject, Input } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

import { startWith } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';

import { DashboardSidenavProps } from '@lib/modules/dashboard/models/dashboard-sidenav-props.model';
import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';
import { DashboardHeaderConfig } from '../dashboard-header/dashboard-header.component';

// TODO move services and models for auth into core
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  /**
   * @description
   * Dashboard header configuration for theming.
   */
  @Input() public headerConfig: DashboardHeaderConfig;
  /**
   * @description
   * Dashboard sidenav configuration for theming.
   */
  @Input() public sideNavConfig: { imgSrc: string, imgAlt: string };
  /**
   * @description
   * Show the sidenav default branding.
   */
  @Input() public showBrand: boolean;
  /**
   * @description
   * List of dashboard details used to populate the side navigation
   * links for routing within the application.
   */
  @Input() public menuItems: DashboardMenuItem[];
  /**
   * @description
   * Whether the dashboard menu items are responsive, and collapse
   * on mobile viewports.
   */
  @Input() public responsiveMenuItems: boolean;
  /**
   * @description
   * Whether the dashboard menu items should display their icons.
   */
  @Input() public showMenuItemIcons: boolean;
  /**
   * @description
   * Redirect URL after logout.
   */
  @Input() public logoutRedirectUrl: string;
  /**
   * @description
   * Side navigation reference.
   */
  @ViewChild('sidenav') public sideNav: MatSidenav;

  public sideNavProps: DashboardSidenavProps;
  public username: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService
  ) {
    this.headerConfig = {
      theme: 'blue',
      showMobileToggle: true
    };
    this.sideNavConfig = {
      imgSrc: '/assets/prime_brand.svg',
      imgAlt: 'PRIME Logo'
    };
    this.responsiveMenuItems = true;
    this.showMenuItemIcons = true;
    this.showBrand = true;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get isDesktop(): boolean {
    return this.viewportService.isDesktop || this.viewportService.isWideDesktop;
  }

  public onAction(dashboardMenuItem: DashboardMenuItem): void {
    // Close on mobile to prevent blocking the screen when routing
    if (dashboardMenuItem instanceof DashboardRouteMenuItem && this.viewportService.isMobile) {
      this.sideNav.close();
    }
  }

  public onLogout() {
    const routePath = this.logoutRedirectUrl ?? this.config.loginRedirectUrl;
    this.authService.logout(routePath);
  }

  public ngOnInit() {
    if (!this.showMenuItemIcons) {
      // Cannot be responsive icons are not being shown
      this.responsiveMenuItems = false;
    }

    // Set the authenticated username for the application header
    this.authService.getUser$()
      .subscribe(({ firstName, lastName }: BcscUser) =>
        this.username = (firstName ? `${firstName} ${lastName}` : lastName)
      );

    // Initialize the side navigation properties, and listen for
    // changes in viewport size
    this.viewportService.onResize()
      .pipe(startWith(this.viewportService.device))
      .subscribe((device: DeviceResolution) =>
        this.sideNavProps = this.getDashboardNavProps(device)
      );
  }

  private getDashboardNavProps(device: DeviceResolution): DashboardSidenavProps {
    switch (device) {
      case DeviceResolution.WIDE:
      case DeviceResolution.DESKTOP:
        return new DashboardSidenavProps('side', true, false);
      case DeviceResolution.TABLET:
        return new DashboardSidenavProps('side', true, false);
      default:
        return new DashboardSidenavProps('over', false, false);
    }
  }
}
