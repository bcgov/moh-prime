import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ActivatedRoute, Router, RouterEvent } from '@angular/router';

import { ViewportService } from '@core/services/viewport.service';
import { RouteStateService } from '@core/services/route-state.service';
import { DashboardNavSection } from '@shared/models/dashboard.model';

@Component({
  selector: 'app-dashboard-nav',
  templateUrl: './dashboard-nav.component.html',
  styleUrls: ['./dashboard-nav.component.scss']
})
export class DashboardNavComponent implements OnInit {
  @Input() public dashboardNavSections: DashboardNavSection[];
  @Input() public showNavItemIcons: boolean;
  @Input() public showNavItemLabels: boolean;
  @Output() public route: EventEmitter<void>;

  constructor(
    private viewportService: ViewportService,
  ) {
    this.route = new EventEmitter<void>();
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get isDesktop(): boolean {
    return this.viewportService.isDesktop || this.viewportService.isWideDesktop;
  }

  public onRoute(): void {
    this.route.emit();
  }

  public ngOnInit(): void { }
}
