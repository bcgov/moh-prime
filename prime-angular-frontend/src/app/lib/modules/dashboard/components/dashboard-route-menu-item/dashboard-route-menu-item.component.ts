import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { ViewportService } from '@core/services/viewport.service';

import { DashboardRouteMenuItem } from '../../models/dashboard-menu-item.model';

@Component({
  selector: 'app-dashboard-route-menu-item',
  templateUrl: './dashboard-route-menu-item.component.html',
  styleUrls: ['./dashboard-route-menu-item.component.scss']
})
export class DashboardRouteMenuItemComponent implements OnInit {
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
   * Current route menu item.
   */
  @Input() routeMenuItem: DashboardRouteMenuItem;
  /**
   * @description
   * Dashboard menu item route emitter.
   */
  @Output() route: EventEmitter<DashboardRouteMenuItem>;

  constructor(
    private viewportService: ViewportService,
  ) {
    this.route = new EventEmitter<DashboardRouteMenuItem>();
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get isDesktop(): boolean {
    return this.viewportService.isDesktop || this.viewportService.isWideDesktop;
  }

  public onRoute(routeMenuItem: DashboardRouteMenuItem) {
    this.route.emit(routeMenuItem);
  }

  public ngOnInit(): void { }
}
