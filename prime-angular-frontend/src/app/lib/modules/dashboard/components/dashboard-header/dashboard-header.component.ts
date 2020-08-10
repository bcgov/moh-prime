import { Component, OnInit, ChangeDetectionStrategy, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-dashboard-header',
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardHeaderComponent implements OnInit {
  @Input() public username: string;
  @Input() public isMobile: boolean;
  @Input() public hasMobileSidemenu: boolean;
  @Output() public toggle: EventEmitter<void>;
  @Output() public logout: EventEmitter<void>;

  constructor() {
    this.hasMobileSidemenu = true;
    this.toggle = new EventEmitter<void>();
    this.logout = new EventEmitter<void>();
  }

  public toggleSidenav(): void {
    this.toggle.emit();
  }

  public onLogout(): void {
    this.logout.emit();
  }

  public ngOnInit(): void { }
}
