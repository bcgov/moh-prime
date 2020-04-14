import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

import { AuthService } from '@auth/shared/services/auth.service';

export abstract class ICollectionNoticeAlert {
  @Input() public showAlert: boolean;
  @Output() public accepted: EventEmitter<void>;

  constructor() {
    this.accepted = new EventEmitter<void>();
    // Default to use alert versus full page
    this.showAlert = true;
  }

  abstract onAccept(): void;
}

@Component({
  selector: 'app-collection-notice-alert',
  templateUrl: './collection-notice-alert.component.html',
  styleUrls: ['./collection-notice-alert.component.scss']
})
export class CollectionNoticeAlertComponent extends ICollectionNoticeAlert implements OnInit {
  public title: string;

  constructor(
    private authService: AuthService
  ) {
    super();

    this.title = 'Collection of Personal Information Notice';
  }

  public get label(): string {
    return (!this.showAlert)
      ? 'Next'
      : 'Ok';
  }

  public get show(): boolean {
    return this.authService.hasJustLoggedIn;
  }

  public onAccept() {
    // Alerts must clear the login indicator to hide it within the view
    // after acceptance, otherwise full collection notices manage this
    // within the parent component
    if (this.showAlert) {
      this.authService.hasJustLoggedIn = false;
    }

    this.accepted.emit();
  }

  public ngOnInit() { }
}
