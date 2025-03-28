import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { UntypedFormGroup, UntypedFormControl, UntypedFormBuilder, Validators } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { HtmlComponent } from '@shared/components/dialogs/content/html/html.component';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { CollectionNoticeService } from '@shared/services/collection-notice.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { asyncValidator } from '@lib/validators/form-async.validators';
import { Observable } from 'rxjs/internal/Observable';

@UntilDestroy()
@Component({
  selector: 'app-prime-enrolment-access',
  templateUrl: './prime-enrolment-access.component.html',
  styleUrls: [
    './prime-enrolment-access.component.scss',
    '../access.component.scss'
  ],
})
export class PrimeEnrolmentAccessComponent implements OnInit {
  @Input() public mode: 'enrolment' | 'community' | 'health-authority';
  @Output() public login: EventEmitter<void>;
  public form: UntypedFormGroup;
  public locationCode: BannerLocationCode;
  public bcscMobileSetupUrl: string;
  public loginCancelled: boolean;
  public bcscHelpDeskUrl: string;
  public enrolmentUrl: string;
  public communitySiteUrl: string;
  public healthAuthorityUrl: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private viewportService: ViewportService,
    private route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private fb: UntypedFormBuilder,
    private collectionNoticeService: CollectionNoticeService,
    private healthAuthorityResource: HealthAuthorityResource,
  ) {
    this.login = new EventEmitter<void>();
    this.locationCode = BannerLocationCode.ENROLMENT_LANDING_PAGE;
    this.bcscMobileSetupUrl = config.bcscMobileSetupUrl;
    this.loginCancelled =
      this.route.snapshot.queryParams.action === 'cancelled';
    this.bcscHelpDeskUrl = this.config.bcscHelpDeskUrl;
    this.enrolmentUrl = "info";
    this.communitySiteUrl = "site";
    this.healthAuthorityUrl = "health-authority";
  }

  public get passcode(): UntypedFormControl {
    return this.form.get('passcode') as UntypedFormControl;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public onLogin(isHA: boolean) {
    if (!isHA || this.form.valid) {
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
          if (isNext) this.login.emit()
        });
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.viewportService.onResize()
      .pipe(untilDestroyed(this))
      .subscribe();
  }

  public goTo(url: string) {
    this.router.navigate([url]);
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      passcode: [
        '',
        [Validators.required],
        asyncValidator(this.checkHAPasscode(), 'validPasscode')
      ],
    });
  }

  private checkHAPasscode(): (passcode: string) => Observable<boolean> {
    return (passcode: string) => this.healthAuthorityResource.checkHealthAuthorityPasscode(passcode);
  }
}
