import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { HtmlComponent } from '@shared/components/dialogs/content/html/html.component';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';

@UntilDestroy()
@Component({
  selector: 'app-prime-enrolment-access',
  templateUrl: './prime-enrolment-access.component.html',
  styleUrls: [
    './prime-enrolment-access.component.scss',
    '../access.component.scss'
  ]
})
export class PrimeEnrolmentAccessComponent implements OnInit {
  @Output() public login: EventEmitter<void>;
  public locationCode: BannerLocationCode;
  public bcscMobileSetupUrl: string;
  public loginCancelled: boolean;
  public bcscHelpDeskUrl: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private viewportService: ViewportService,
    private route: ActivatedRoute,
    private dialog: MatDialog
  ) {
    this.login = new EventEmitter<void>();
    this.locationCode = BannerLocationCode.ENROLMENT_LANDING_PAGE;
    this.bcscMobileSetupUrl = config.bcscMobileSetupUrl;
    this.loginCancelled =
      this.route.snapshot.queryParams.action === 'cancelled';
    this.bcscHelpDeskUrl = this.config.bcscHelpDeskUrl;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public onLogin() {
    const data: DialogOptions = {
      title: 'Collection of Personal Information Notice',
      //      title: 'Collection Notice',
      component: HtmlComponent,
      data: {
        content: `<p>
            The personal information you provide to the PRIME application is collected by the British Columbia Ministry
            of Health under the authority of s. 26(a) and 26(c) of the Freedom of Information and Protection of Privacy
            Act (FOIPPA) and s. 22(1)(b) of the Pharmaceutical Services Act for the purpose of managing your access to,
            and use of, PharmaNet. If you have any questions about the collection or use of this information, call
          </p>
          <p>
            Director, Information and PharmaNet Innovation at <a href="tel:+${this.config.phoneNumbers.director}">${this.config.phoneNumbers.director}</a>
            or <a href="mailto:${this.config.prime.email}">${this.config.prime.email}</a>.
          </p>`,
      },
      actionText: 'Next',
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((isNext: boolean) => {
        if (isNext) this.login.emit()
      });
  }

  public ngOnInit(): void {
    this.viewportService.onResize()
      .pipe(untilDestroyed(this))
      .subscribe();
  }
}
