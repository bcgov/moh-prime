import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { ViewportService } from '@core/services/viewport.service';

@UntilDestroy()
@Component({
  selector: 'app-simple-access',
  templateUrl: './simple-access.component.html',
  styleUrls: [
    './simple-access.component.scss',
    '../access.component.scss'
  ]
})
export class SimpleAccessComponent implements OnInit {
  @Input() public title: string;
  @Input() public loginLabel: string;
  @Input() public showLogo: boolean;
  /**
   * @description
   * Disable authentication.
   */
  @Input() public disableLogin: boolean;
  @Output() public login: EventEmitter<void>;

  constructor(
    private viewportService: ViewportService
  ) {
    this.login = new EventEmitter<void>();
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public onLogin() {
    if (this.disableLogin) {
      return;
    }

    this.login.emit();
  }

  public ngOnInit(): void {
    this.viewportService.onResize()
      .pipe(untilDestroyed(this))
      .subscribe();
  }
}
