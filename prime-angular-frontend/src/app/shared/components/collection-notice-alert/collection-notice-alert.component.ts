import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-collection-notice-alert',
  templateUrl: './collection-notice-alert.component.html',
  styleUrls: ['./collection-notice-alert.component.scss']
})
export class CollectionNoticeAlertComponent implements OnInit {
  @Input() public showAlert: boolean;
  @Output() public accepted: EventEmitter<void>;

  constructor(
    private authService: AuthService
  ) {
    this.accepted = new EventEmitter<void>();
  }

  public get label(): string {
    return (!this.showAlert)
      ? 'Next'
      : 'Ok';
  }

  public show(): boolean {
    return this.authService.hasJustLoggedIn;
  }

  public onAccept() {
    this.accepted.emit();
  }

  public ngOnInit() { }
}
