import { Component, OnInit, ViewChild, Inject, Input } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

import { startWith } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';

import { DashboardMenuProps } from '@lib/modules/dashboard/models/dashboard-menu-props.model';
import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
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
   * List of dashboard details used to populate the side navigation
   * links for routing within the application.
   */
  @Input() public dashboardMenuItems: DashboardMenuItem[];
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

  public sideNavProps: DashboardMenuProps;
  public username: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService
  ) {
    this.responsiveMenuItems = true;
    this.showMenuItemIcons = true;
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
      .subscribe(({ firstName, lastName }: User) =>
        this.username = `${firstName} ${lastName}`
      );

    // Initialize the side navigation properties, and listen for
    // changes in viewport size
    this.viewportService.onResize()
      .pipe(startWith(this.viewportService.device))
      .subscribe((device: DeviceResolution) =>
        this.sideNavProps = this.getDashboardNavProps(device)
      );
  }

  private getDashboardNavProps(device: DeviceResolution): DashboardMenuProps {
    switch (device) {
      case DeviceResolution.WIDE:
      case DeviceResolution.DESKTOP:
        return new DashboardMenuProps('side', true, false);
      case DeviceResolution.TABLET:
        return new DashboardMenuProps('side', true, false);
      default:
        return new DashboardMenuProps('over', false, false);
    }
  }
}
