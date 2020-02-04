import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

import { map, distinctUntilChanged, pairwise } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { LoggerService } from '@core/services/logger.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { DashboardNavSection } from '@shared/models/dashboard.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { AuthRoutes } from '@auth/auth.routes';
import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidenav', { static: false }) public sideNav: MatSidenav;

  public dashboardNavSections: DashboardNavSection[];
  public sideNavProps: {
    mode: string,
    opened: boolean,
    fixedInViewport: boolean,
    showText: boolean
  };

  public username: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private authService: AuthService,
    private viewportService: ViewportService,
    private enrolmentService: EnrolmentService,
    private router: Router,
    private logger: LoggerService
  ) { }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get isDesktop(): boolean {
    return this.viewportService.isDesktop || this.viewportService.isWideDesktop;
  }

  public routeTo(route: string) {
    if (this.viewportService.isMobile) {
      this.sideNav.close();
    }

    this.router.navigate([route]);
  }

  public onRoute(): void {
    if (this.viewportService.isMobile) {
      this.sideNav.close();
    }
  }

  public onLogout() {
    if (this.authService.isAdmin()) {
      this.authService.logout(`${this.config.loginRedirectUrl}/${AuthRoutes.ADMIN}`);
    } else {
      this.authService.logout(this.config.loginRedirectUrl);
    }
  }

  public async ngOnInit() {
    // Initialize the side navigation based on the type of user
    this.dashboardNavSections = this.getSideNavSections();

    if (this.authService.isEnrollee()) {
      // Listen for changes to the current enrolment status to update
      // the side navigation based on enrollee progression
      this.enrolmentService.enrolment$
        .pipe(
          // Reduce noise from enrollee profile updates, and
          // only focus on the current status
          map((enrolment: Enrolment) =>
            (enrolment && enrolment.currentStatus)
              ? enrolment.currentStatus.status.code
              : null
          ),
          distinctUntilChanged(),
          pairwise()
        )
        .subscribe(([prevCurrentStatus, nextCurrentStatus]) =>
          this.dashboardNavSections = this.getSideNavSections()
        );
    }

    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);
    // Listen for viewport onresize changes
    this.viewportService.onResize()
      .subscribe((device: string) => this.setSideNavProps(device));

    const user = await this.authService.getUser();
    this.username = `${user.firstName} ${user.lastName}`;
  }

  private getSideNavSections(): DashboardNavSection[] {
    return (this.authService.isAdjudicator() || this.authService.isAdmin())
      ? this.getAdjudicationSideNavSections()
      : this.getEnrolleeSideNavSections();
  }

  // TODO Refactor side nav sections and logic when the next set of
  // status changes are implemented
  private getEnrolleeSideNavSections(): DashboardNavSection[] {
    const enrolment = this.enrolmentService.enrolment;
    const enrolmentStatus = (enrolment)
      ? enrolment.currentStatus.status.code
      : EnrolmentStatus.IN_PROGRESS;
    // Indicates the position of the enrollee within their initial enrolment, which
    // provides a status hook with greater granularity than the enrolment statuses
    const progressStatus = (enrolment)
      ? enrolment.progressStatus
      : ProgressStatus.STARTED;
    const statusIcons = this.getEnrolmentStatusIcons(enrolmentStatus, progressStatus);

    return [
      {
        header: 'Enrolment',
        showHeader: false,
        items: [
          {
            name: 'PRIME Profile',
            icon: statusIcons.enrollee,
            route: EnrolmentRoutes.OVERVIEW,
            showItem: true,
            // Will never be disabled, so has been explicitly set
            disabled: (
              progressStatus !== ProgressStatus.FINISHED ||
              [
                EnrolmentStatus.IN_PROGRESS,
                EnrolmentStatus.SUBMITTED,
                EnrolmentStatus.ADJUDICATED_APPROVED,
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            ),
            forceActive: (
              // Highlight the profile when in these states
              [EnrolmentStatus.IN_PROGRESS].includes(enrolmentStatus)
            )
          },
          {
            name: 'Terms of Access',
            icon: statusIcons.accessAgreement,
            route: (enrolmentStatus === EnrolmentStatus.ACCEPTED_TOS)
              ? EnrolmentRoutes.CURRENT_ACCESS_TERM
              : EnrolmentRoutes.TERMS_OF_ACCESS,
            showItem: true,
            disabled: (
              [
                EnrolmentStatus.IN_PROGRESS,
                EnrolmentStatus.SUBMITTED,
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            ),
            forceActive: (
              // Highlight the terms of access when in these states
              [EnrolmentStatus.SUBMITTED].includes(enrolmentStatus)
            )
          },
          {
            name: 'PharmaNet Enrolment Certificate',
            icon: statusIcons.certificate,
            route: EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE,
            showItem: true,
            disabled: (
              [
                EnrolmentStatus.IN_PROGRESS,
                EnrolmentStatus.SUBMITTED,
                EnrolmentStatus.ADJUDICATED_APPROVED,
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            )
          }
        ]
      },
      {
        items: [
          {
            name: 'PharmaNet Transactions',
            icon: (
              progressStatus !== ProgressStatus.FINISHED ||
              [
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'date_range',
            route: EnrolmentRoutes.PHARMANET_TRANSACTIONS,
            showItem: true,
            disabled: (
              progressStatus !== ProgressStatus.FINISHED ||
              [
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            ),
            deemphasize: (!enrolment || (enrolment && !enrolment.profileCompleted)) ? true : false
          },
          {
            name: 'PRIME Transaction History',
            icon: (
              progressStatus !== ProgressStatus.FINISHED ||
              [
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'history',
            route: EnrolmentRoutes.ACCESS_TERMS,
            showItem: true,
            disabled: (
              progressStatus !== ProgressStatus.FINISHED ||
              [
                EnrolmentStatus.DECLINED,
                EnrolmentStatus.DECLINED_TOS
              ].includes(enrolmentStatus)
            ),
            deemphasize: (!enrolment || (enrolment && !enrolment.profileCompleted)) ? true : false
          }
        ]
      },
    ];
  }

  private getEnrolmentStatusIcons(
    enrolmentStatus: EnrolmentStatus,
    progressStatus: ProgressStatus
  ) {
    let enrollee = 'assignment_ind';
    let accessAgreement = 'assignment';
    let certificate = 'card_membership';

    if (progressStatus !== ProgressStatus.FINISHED) {
      enrollee = 'assignment_turned_in';
      accessAgreement = 'lock';
      certificate = 'lock';

      switch (enrolmentStatus) {
        case EnrolmentStatus.IN_PROGRESS:
          enrollee = 'assignment_ind';
          break;
        case EnrolmentStatus.SUBMITTED:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatus.ADJUDICATED_APPROVED:
          accessAgreement = 'assignment';
          break;
        case EnrolmentStatus.DECLINED:
          enrollee = 'highlight_off';
          break;
        case EnrolmentStatus.DECLINED_TOS:
          accessAgreement = 'highlight_off';
          break;
      }
    } else {
      switch (enrolmentStatus) {
        case EnrolmentStatus.SUBMITTED:
          accessAgreement = 'lock';
          certificate = 'lock';
          break;
        case EnrolmentStatus.ADJUDICATED_APPROVED:
          enrollee = 'lock';
          certificate = 'lock';
          break;
        case EnrolmentStatus.DECLINED:
        case EnrolmentStatus.DECLINED_TOS:
          enrollee = 'lock';
          accessAgreement = 'lock';
          certificate = 'lock';
          break;
      }
    }

    return { enrollee, accessAgreement, certificate };
  }

  private getAdjudicationSideNavSections(): DashboardNavSection[] {
    return [
      {
        header: 'Enrolments',
        showHeader: false,
        items: [
          {
            name: 'Enrolments',
            icon: 'format_list_bulleted',
            route: AdjudicationRoutes.ENROLMENTS,
            showItem: true
          }
        ]
      }
    ];
  }

  private setSideNavProps(device: string) {
    if (device === DeviceResolution.MOBILE) {
      this.sideNavProps = {
        mode: 'over',
        opened: false,
        fixedInViewport: false,
        showText: false
      };
    } else if (device === DeviceResolution.TABLET) {
      this.sideNavProps = {
        mode: 'side',
        opened: true,
        fixedInViewport: false,
        showText: false
      };
    } else {
      this.sideNavProps = {
        mode: 'side',
        opened: true,
        fixedInViewport: false,
        showText: true
      };
    }
  }
}
