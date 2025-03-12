import { AfterViewInit, Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';

import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
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
import { filter, first, from, tap } from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'app-prime-enrolment-access',
  templateUrl: './prime-enrolment-access.component.html',
  styleUrls: [
    './prime-enrolment-access.component.scss',
    '../access.component.scss'
  ],
})
export class PrimeEnrolmentAccessComponent implements OnInit, AfterViewInit {
  @ViewChild('passcodeInput') passcodeInput: ElementRef<HTMLInputElement>;
  @ViewChild('siteTitleDiv') siteTitleDiv: ElementRef<HTMLDivElement>;

  @Input() public mode: 'enrolment' | 'community' | 'health-authority';
  @Output() public login: EventEmitter<void>;
  public form: FormGroup;
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
    private fb: FormBuilder,
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


    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      window.scrollTo(500, 0);
    });
  }

  public get passcode(): FormControl {
    return this.form.get('passcode') as FormControl;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public onEnrolClick() {
    this.enrolmentLogin();
  }

  public onAlreadyEnrolledLinkClick(event: Event) {
    event.preventDefault();
    this.enrolmentLogin();
  }

  public onSiteRegistrationClick() {
    this.communitySiteLogin();
  }

  public onHealthAuthoritySiteRegistrationClick(event: Event) {
    event.preventDefault();
    if (this.mode !== 'health-authority') {
      this.router.navigate([this.healthAuthorityUrl]);
    } else {
      this.router.navigate([this.communitySiteUrl]);
    }
  }

  public onPasscodeSubmit() {
    if (this.form.valid) {
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

  public onPasscodeCancel() {
    this.router.navigate([this.communitySiteUrl]);
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.viewportService.onResize()
      .pipe(untilDestroyed(this))
      .subscribe();

    if (this.route.snapshot.queryParams?.login === 'true') {
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

  public ngAfterViewInit(): void {

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

  private enrolmentLogin() {
    if (this.mode === 'enrolment') {
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
    } else {
      this.router.navigate([this.enrolmentUrl], { queryParams: { login: true } });
    }
  }

  private communitySiteLogin() {
    if (this.mode === 'community') {
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
    } else {
      this.router.navigate([this.communitySiteUrl], { queryParams: { login: true } });
    }
  }

}
