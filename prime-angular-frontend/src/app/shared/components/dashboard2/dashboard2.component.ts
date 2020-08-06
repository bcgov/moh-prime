import { Component, OnInit, ViewChild, Inject, Input } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

import { startWith } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';
import { DashboardNavSection, DashboardNavProps } from '@shared/models/dashboard.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { User } from '@auth/shared/models/user.model';

@Component({
  selector: 'app-dashboard2',
  templateUrl: './dashboard2.component.html',
  styleUrls: ['./dashboard2.component.scss']
})
export class Dashboard2Component implements OnInit {
  /**
   * @description
   * Configuration for dashboard navigation links.
   */
  @Input() public dashboardNavSections: DashboardNavSection[];
  /**
   * @description
   * Whether to show dashboard navigation link icons. If icons
   * are not shown the logo will become a fixed size matching
   * the large viewport.
   */
  @Input() public showNavItemIcons: boolean;
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

  public sideNavProps: DashboardNavProps;
  public username: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService
  ) {
    this.showNavItemIcons = false;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get isDesktop(): boolean {
    return this.viewportService.isDesktop || this.viewportService.isWideDesktop;
  }

  public onRoute(): void {
    // Close on mobile to prevent blocking the screen
    if (this.viewportService.isMobile) {
      this.sideNav.close();
    }
  }

  public onLogout() {
    const routePath = this.logoutRedirectUrl ?? this.config.loginRedirectUrl;
    this.authService.logout(routePath);
  }

  public ngOnInit() {
    // Set the authenticated username for the application header
    this.authService.getUser$()
      .subscribe((user: User) =>
        this.username = `${user.firstName} ${user.lastName}`
      );

    // Initialize the side navigation properties, and listen for
    // changes in viewport size
    this.viewportService.onResize()
      .pipe(startWith(this.viewportService.device))
      .subscribe((device: DeviceResolution) =>
        this.sideNavProps = this.getDashboardNavProps(device)
      );
  }

  private getDashboardNavProps(device: DeviceResolution): DashboardNavProps {
    switch (device) {
      case DeviceResolution.WIDE:
      case DeviceResolution.DESKTOP:
        return new DashboardNavProps('side', true, false, this.showNavItemIcons, true);
      case DeviceResolution.TABLET:
        return new DashboardNavProps('side', true, false, this.showNavItemIcons, false);
      default:
        return new DashboardNavProps('over', false, false, this.showNavItemIcons, false);
    }
  }
}
