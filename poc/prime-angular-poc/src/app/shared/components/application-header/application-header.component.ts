import { Component, OnInit, ChangeDetectionStrategy, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-application-header',
  templateUrl: './application-header.component.html',
  styleUrls: ['./application-header.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ApplicationHeaderComponent implements OnInit {
  @Input() public username: string;
  @Input() public isMobile: boolean;
  @Output() public toggle: EventEmitter<void>;
  @Output() public action: EventEmitter<void>;

  constructor() {
    this.toggle = new EventEmitter<void>();
    this.action = new EventEmitter<void>();
  }

  public toggleSidenav() {
    this.toggle.emit();
  }

  public onAction() {
    this.action.emit();
  }

  public ngOnInit() { }
}
