import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { ViewportService } from '@core/services/viewport.service';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { SiteRegistrationTypeEnum } from '@health-auth/shared/enums/site-registration-type.enum';

@UntilDestroy()
@Component({
  selector: 'app-site-reg-access',
  templateUrl: './site-reg-access.component.html',
  styleUrls: [
    './site-reg-access.component.scss',
    '../access.component.scss'
  ]
})
export class SiteRegAccessComponent implements OnInit {
  /**
   * @description
   * Whether the site registration login will handle a single, or
   * multiple types of site registration authentication.
   *
   * NOTE:
   * Temporary until site registrations are integrated together, and
   * single site registration can be dropped
   */
  @Input() public mode: 'single' | 'multiple';
  /**
   * @description
   * Disable authentication.
   *
   * TODO remove when feature flag is removed on health authority site registration
   */
  @Input() public disableLogin: boolean;
  /**
   * @description
   * Emit the login event with the type of site registration
   * that is being authenticated.
   */
  @Output() public login: EventEmitter<SiteRegistrationTypeEnum>;
  public locationCode: BannerLocationCode;
  public SiteRegistrationTypeEnum = SiteRegistrationTypeEnum;

  constructor(
    private viewportService: ViewportService
  ) {
    this.mode = 'single';
    this.login = new EventEmitter<SiteRegistrationTypeEnum>();
    this.locationCode = BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public onLogin(type: SiteRegistrationTypeEnum = SiteRegistrationTypeEnum.COMM_PHARMACY_PRACTICE) {
    this.login.emit(type);
  }

  public ngOnInit(): void {
    this.viewportService.onResize()
      .pipe(untilDestroyed(this))
      .subscribe();
  }
}
