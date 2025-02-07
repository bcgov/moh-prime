import { Component, OnInit, ChangeDetectionStrategy, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeaderComponent implements OnInit {
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

  public toggleSidenav() {
    this.toggle.emit();
  }

  public onLogout() {
    this.logout.emit();
  }

  public ngOnInit() { }
}
