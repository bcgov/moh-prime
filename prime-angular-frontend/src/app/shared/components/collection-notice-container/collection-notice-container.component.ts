import { Component, OnInit, EventEmitter, Output, Input, Directive } from '@angular/core';

import { AuthService } from '@auth/shared/services/auth.service';

@Directive()
// eslint-disable-next-line @angular-eslint/directive-class-suffix
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
    private authService: AuthService
  ) {
    super();

    this.title = 'Collection of Personal Information Notice';
  }

  public get label(): string {
    return (this.isFull) ? 'Next' : 'Continue';
  }

  public get show(): boolean {
    return this.authService.hasJustLoggedIn;
  }

  public onAccept() {
    // Alerts must clear the login indicator to hide it within the view
    // after acceptance, otherwise full collection notices manage this
    // within the parent component
    if (!this.isFull) {
      this.authService.hasJustLoggedIn = false;
    }

    this.accepted.emit();
  }

  public ngOnInit() { }
}
