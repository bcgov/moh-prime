import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';

import { merge } from 'rxjs';
import { map, distinctUntilChanged } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { RouteStateService } from '@core/services/route-state.service';
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
import { SiteRoutes } from 'app/modules/site-registration/site-registration.routes';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidenav') public sideNav: MatSidenav;

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
    private router: Router,
    private routeStateService: RouteStateService,
    private authService: AuthService,
    private viewportService: ViewportService,
    private enrolmentService: EnrolmentService,
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
    let routePath = this.config.loginRedirectUrl;

    if (this.authService.hasAdminView()) {
      routePath = `${routePath}/${AuthRoutes.ADMIN}`;
    }

    this.authService.logout(routePath);
  }

  public async ngOnInit() {
    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);

    // Initialize the side navigation based on the type of user
    this.dashboardNavSections = this.getSideNavSections();

    if (await this.authService.isEnrollee()) {
      // Listen for changes to the current enrolment status to update
      // the side navigation based on enrollee progression
      merge(
        this.enrolmentService.enrolment$
          .pipe(
            // Reduce noise from enrollee profile updates, and
            // only focus on the current status
            map((enrolment: Enrolment) =>
              (enrolment && enrolment.currentStatus)
                ? enrolment.currentStatus.statusCode
                : null
            ),
            distinctUntilChanged()
          ),
        this.routeStateService.onNavigationEnd()
      )
        .subscribe(() =>
          this.dashboardNavSections = this.getSideNavSections()
        );
    }

    this.viewportService.onResize()
      .subscribe((device: string) => this.setSideNavProps(device));

    const user = await this.authService.getUser();
    this.username = `${user.firstName} ${user.lastName}`;
  }

  private getSideNavSections(): DashboardNavSection[] {
    const currentBaseRoute = this.router.url.slice(1).split('/')[0];
    if (this.authService.hasAdminView()) {
      return this.getAdjudicationSideNavSections();
      // TODO use of routes creates coupling between modules
    } else if (this.authService.isRegistrant() && currentBaseRoute === SiteRoutes.MODULE_PATH) {
      return this.getRegistrantSideNavSections();
    } else {
      return this.getEnrolleeSideNavSections();
    }
  }

  private getEnrolleeSideNavSections(): DashboardNavSection[] {
    const enrolment = this.enrolmentService.enrolment;
    const enrolmentStatus = (enrolment)
      ? enrolment.currentStatus.statusCode
      : EnrolmentStatus.EDITABLE;
    // Check if the enrollee is within their initial enrolment
    const hasAcceptedAtLeastOneToa = (enrolment)
      ? !!enrolment.expiryDate
      : false;
    const statusIcons = this.getEnrolmentStatusIcons(enrolmentStatus, hasAcceptedAtLeastOneToa);
    const currentRoute = this.router.url.slice(1).split('/')[1];

    const termsOfAccessRoute = (enrolmentStatus === EnrolmentStatus.UNDER_REVIEW)
      ? EnrolmentRoutes.SUBMISSION_CONFIRMATION
      : (enrolmentStatus === EnrolmentStatus.REQUIRES_TOA)
        ? EnrolmentRoutes.PENDING_ACCESS_TERM
        : EnrolmentRoutes.CURRENT_ACCESS_TERM;

    return [
      {
        items: [
          {
            name: 'PRIME Profile',
            icon: statusIcons.enrollee,
            route: EnrolmentRoutes.OVERVIEW,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED,
                EnrolmentStatus.DECLINED
              ].includes(enrolmentStatus)
            ),
            forceActive: EnrolmentRoutes.enrolmentProfileRoutes().includes(currentRoute)
          },
          {
            name: 'Terms of Access',
            icon: statusIcons.accessAgreement,
            route: termsOfAccessRoute,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED,
                EnrolmentStatus.DECLINED
              ].includes(enrolmentStatus)
            ),
            forceActive: [
              EnrolmentRoutes.PENDING_ACCESS_TERM,
              EnrolmentRoutes.CURRENT_ACCESS_TERM
            ].includes(currentRoute)
          },
          {
            name: 'Next Steps to get PharmaNet',
            icon: statusIcons.certificate,
            route: EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED,
                EnrolmentStatus.DECLINED
              ].includes(enrolmentStatus)
            )
          }
        ]
      },
      {
        items: [
          // TODO removed until the page has been implemented
          // {
          //   name: 'PharmaNet Transactions',
          //   icon: (
          //     !hasAcceptedAtLeastOneToa ||
          //     [
          //       EnrolmentStatus.LOCKED,
          //       EnrolmentStatus.DECLINED
          //     ].includes(enrolmentStatus)
          //   )
          //     ? 'lock'
          //     : 'date_range',
          //   route: EnrolmentRoutes.PHARMANET_TRANSACTIONS,
          //   showItem: true,
          //   disabled: (
          //     !hasAcceptedAtLeastOneToa ||
          //     [
          //       EnrolmentStatus.LOCKED,
          //       EnrolmentStatus.DECLINED
          //     ].includes(enrolmentStatus)
          //   ),
          //   deemphasize: this.enrolmentService.isInitialEnrolment
          // },
          {
            name: 'PRIME History',
            icon: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED,
                EnrolmentStatus.DECLINED
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'history',
            route: EnrolmentRoutes.ACCESS_TERMS,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED,
                EnrolmentStatus.DECLINED
              ].includes(enrolmentStatus)
            ),
            deemphasize: this.enrolmentService.isInitialEnrolment
          }
        ]
      },
    ];
  }

  private getEnrolmentStatusIcons(enrolmentStatus: EnrolmentStatus, hasAcceptedAtLeastOneToa: boolean) {
    let enrollee = 'assignment_ind';
    let accessAgreement = 'assignment';
    let certificate = 'mail';

    if (!hasAcceptedAtLeastOneToa) {
      // Default icons when performing initial enrolment
      accessAgreement = 'lock';
      certificate = 'lock';

      switch (enrolmentStatus) {
        case EnrolmentStatus.EDITABLE:
          break;
        case EnrolmentStatus.UNDER_REVIEW:
        case EnrolmentStatus.REQUIRES_TOA:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatus.LOCKED:
          enrollee = 'lock';
          break;
        case EnrolmentStatus.DECLINED:
          enrollee = 'lock';
          accessAgreement = 'lock';
          certificate = 'lock';
          break;
      }
    } else {
      switch (enrolmentStatus) {
        case EnrolmentStatus.EDITABLE:
          break;
        case EnrolmentStatus.UNDER_REVIEW:
        case EnrolmentStatus.REQUIRES_TOA:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatus.LOCKED:
          enrollee = 'lock';
          accessAgreement = 'lock';
          certificate = 'lock';
          break;
        case EnrolmentStatus.DECLINED:
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
        items: [
          {
            name: 'PRIME Enrollees',
            icon: 'people',
            route: AdjudicationRoutes.ENROLLEES,
            showItem: true
          },
          {
            name: 'Site Registrations',
            icon: 'store',
            route: AdjudicationRoutes.SITE_REGISTRATIONS,
            showItem: true
          }
        ]
      }
    ];
  }

  private getRegistrantSideNavSections(): DashboardNavSection[] {
    return [
      {
        items: [
          {
            name: 'Site Management',
            icon: 'store',
            route: SiteRoutes.SITE_MANAGEMENT,
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
