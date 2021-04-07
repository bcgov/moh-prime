import { Component, OnInit, ChangeDetectionStrategy, Output, EventEmitter, Input } from '@angular/core';

export interface DashboardHeaderConfig {
  theme?: 'blue' | 'white';
  // TODO change name to reflect a default that will show the toggle
  showMobileToggle?: boolean;
}

@Component({
  selector: 'app-dashboard-header',
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardHeaderComponent implements OnInit {
  /**
   * @description
   * Dashboard header configuration for theming.
   */
  @Input() public headerConfig: DashboardHeaderConfig;
  /**
   * @description
   * Username displayed in the dashboard header.
   */
  @Input() public username: string;
  /**
   * @description
   * Indicator that viewport dimensions match a
   * mobile device.
   *
   * NOTE: showMobileToggle will on be displayed when
   * the mobile device viewport dimension are provided.
   */
  @Input() public isMobile: boolean;
  /**
   * @description
   * Event emission of toggling the dashboard sidenav
   * drawer action.
   */
  @Output() public toggle: EventEmitter<void>;
  /**
   * @description
   * Event emission of logout action.
   */
  @Output() public logout: EventEmitter<void>;

  public brandImgSrc: string;

  constructor() {
    this.headerConfig = {
      theme: 'blue',
      showMobileToggle: true
    };
    this.toggle = new EventEmitter<void>();
    this.logout = new EventEmitter<void>();
  }

  public toggleSidenav(): void {
    this.toggle.emit();
  }

  public onLogout(): void {
    this.logout.emit();
  }

  public ngOnInit(): void {
    this.brandImgSrc = (this.headerConfig.theme === 'white')
      ? '/assets/gov_bc_logo_white.jpeg'
      : '/assets/gov_bc_logo_blue.png';
  }
}
