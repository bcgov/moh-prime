import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';

import { AuthenticationService } from '@auth/shared/services/authentication.service';

export abstract class AbstractCollectionNoticeAlert {
  @Input() public isFull: boolean;
  @Output() public accepted: EventEmitter<void>;

  constructor() {
    this.accepted = new EventEmitter<void>();
  }

  abstract onAccept(): void;
}

@Component({
  selector: 'app-collection-notice-container',
  templateUrl: './collection-notice-container.component.html',
  styleUrls: ['./collection-notice-container.component.scss']
})
export class CollectionNoticeContainerComponent extends AbstractCollectionNoticeAlert implements OnInit {
  public title: string;

  constructor(
    private authenticationService: AuthenticationService
  ) {
    super();

    this.title = 'Collection of Personal Information Notice';
  }

  public get label(): string {
    return (this.isFull) ? 'Next' : 'Ok';
  }

  public get show(): boolean {
    return this.authenticationService.hasJustLoggedIn;
  }

  public onAccept() {
    // Alerts must clear the login indicator to hide it within the view
    // after acceptance, otherwise full collection notices manage this
    // within the parent component
    if (!this.isFull) {
      this.authenticationService.hasJustLoggedIn = false;
    }

    this.accepted.emit();
  }

  public ngOnInit() { }
}
