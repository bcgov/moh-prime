import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

import { DashboardMenuItem, DashboardRouteMenuItem } from '@lib/modules/dashboard/models/dashboard-menu-item.model';

@Component({
  selector: 'app-dashboard-menu',
  templateUrl: './dashboard-menu.component.html',
  styleUrls: ['./dashboard-menu.component.scss']
})
export class DashboardMenuComponent implements OnInit {
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
   * Dashboard menu item action emitter.
   */
  @Output() public action: EventEmitter<DashboardMenuItem>;

  constructor() {
    this.action = new EventEmitter<DashboardMenuItem>();
  }

  public isDashboardRouteMenuItem(menuItem: DashboardMenuItem): boolean {
    return menuItem instanceof DashboardRouteMenuItem;
  }

  public onAction(dashboardMenuItem: DashboardMenuItem): void {
    this.action.emit(dashboardMenuItem);
  }

  public ngOnInit(): void { }
}
