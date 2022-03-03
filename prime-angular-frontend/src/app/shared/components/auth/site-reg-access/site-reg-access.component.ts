import { Component, EventEmitter, OnInit, Output, Input, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { HtmlComponent } from '@shared/components/dialogs/content/html/html.component';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { CollectionNoticeService } from '@shared/services/collection-notice.service';
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
   */
  @Input() public disableLogin: boolean;
  /**
   * @description
   * Emit the login event with the type of site registration
   * that is being authenticated.
   */
  @Output() public login: EventEmitter<SiteRegistrationTypeEnum>;
  public locationCode: BannerLocationCode;
  public bcscMobileSetupUrl: string;
  public SiteRegistrationTypeEnum = SiteRegistrationTypeEnum;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private viewportService: ViewportService,
    private dialog: MatDialog,
    private collectionNoticeService: CollectionNoticeService
  ) {
    this.mode = 'single';
    this.login = new EventEmitter<SiteRegistrationTypeEnum>();
    this.locationCode = BannerLocationCode.SITE_REGISTRATION_LANDING_PAGE;
    this.bcscMobileSetupUrl = config.bcscMobileSetupUrl;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public onLogin(type: SiteRegistrationTypeEnum = SiteRegistrationTypeEnum.COMM_PHARMACY_PRACTICE) {
    if (this.disableLogin) {
      return;
    }

    const data: DialogOptions = {
      title: this.collectionNoticeService.Title,
      component: HtmlComponent,
      data: {
        content: this.collectionNoticeService.ContentToRender,
      },
      actionText: 'Next',
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((isNext: boolean) => {
        if (isNext) this.login.emit(type)
      });
  }

  public ngOnInit(): void {
    this.viewportService.onResize()
      .pipe(untilDestroyed(this))
      .subscribe();
  }
}
