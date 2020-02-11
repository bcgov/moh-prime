import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
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
    private route: ActivatedRoute,
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
              ? enrolment.currentStatus.statusCode
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

  private getEnrolleeSideNavSections(): DashboardNavSection[] {
    const enrolment = this.enrolmentService.enrolment;
    const enrolmentStatus = (enrolment)
      ? enrolment.currentStatus.statusCode
      : EnrolmentStatus.ACTIVE;
    // Check if the enrollee is within their initial enrolment
    const hasAcceptedAtLeastOneToa = (enrolment)
      ? !!enrolment.expiryDate
      : false;
    const statusIcons = this.getEnrolmentStatusIcons(enrolmentStatus, hasAcceptedAtLeastOneToa);

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
            disabled: (
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.UNDER_REVIEW,
                EnrolmentStatus.REQUIRES_TOA,
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            ),
            // forceActive: (
            //   // TODO highlight needs to be based on routes... use child routes for profile?
            //   // Highlight the profile when in these states
            //   // EnrolmentRoutes.enrolmentProfileRoutes().includes()
            // )
          },
          {
            name: 'Terms of Access',
            icon: statusIcons.accessAgreement,
            route: EnrolmentRoutes.CURRENT_ACCESS_TERM,
            showItem: true,
            disabled: (
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.UNDER_REVIEW,
                EnrolmentStatus.REQUIRES_TOA,
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            )
          },
          {
            name: 'PharmaNet Enrolment Certificate',
            icon: statusIcons.certificate,
            route: EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE,
            showItem: true,
            disabled: (
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.UNDER_REVIEW,
                EnrolmentStatus.REQUIRES_TOA,
                EnrolmentStatus.LOCKED
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
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'date_range',
            route: EnrolmentRoutes.PHARMANET_TRANSACTIONS,
            showItem: true,
            disabled: (
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            ),
            deemphasize: (!enrolment || (enrolment && !enrolment.profileCompleted)) ? true : false
          },
          {
            name: 'PRIME Transaction History',
            icon: (
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'history',
            route: EnrolmentRoutes.ACCESS_TERMS,
            showItem: true,
            disabled: (
              hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            ),
            deemphasize: (!enrolment || (enrolment && !enrolment.profileCompleted)) ? true : false
          }
        ]
      },
    ];
  }

  private getEnrolmentStatusIcons(enrolmentStatus: EnrolmentStatus, hasAcceptedAtLeastOneToa: boolean) {
    let enrollee = 'assignment_ind';
    let accessAgreement = 'assignment';
    let certificate = 'card_membership';

    if (hasAcceptedAtLeastOneToa) {
      // Default icons when performing initial enrolment
      enrollee = 'assignment_turned_in';
      accessAgreement = 'lock';
      certificate = 'lock';

      switch (enrolmentStatus) {
        case EnrolmentStatus.ACTIVE:
          enrollee = 'assignment_ind';
          break;
        case EnrolmentStatus.UNDER_REVIEW:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatus.REQUIRES_TOA:
          accessAgreement = 'assignment';
          break;
        case EnrolmentStatus.LOCKED:
          enrollee = 'lock';
          break;
      }
    } else {
      switch (enrolmentStatus) {
        case EnrolmentStatus.UNDER_REVIEW:
        case EnrolmentStatus.REQUIRES_TOA:
          // Prevent viewing current TOA since it is assumed to be
          // changing at this point, but can access it in history
          enrollee = 'lock';
          break;
        case EnrolmentStatus.LOCKED:
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
