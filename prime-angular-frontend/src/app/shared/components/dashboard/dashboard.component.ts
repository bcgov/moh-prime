import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router, ActivatedRoute, RouterEvent } from '@angular/router';
import { MatSidenav } from '@angular/material';

import { Observable } from 'rxjs';
import { map, distinctUntilChanged, pairwise, startWith } from 'rxjs/operators';

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

    if (this.authService.isAdmin()) {
      routePath = `${routePath}/${AuthRoutes.ADMIN}`;
    }

    this.authService.logout(routePath);
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

    // Placed outside ngOnInit on purpose to avoid issues with timing in the lifecycle hook
    const currentRoutePath$ = this.routeStateService.routePath$
      .pipe(
        // Provide a default since the navigation end event of the router
        // doesn't occur as the application is initially loaded
        startWith(this.router.url),
        // Only care about the second parameter to determine route access, and
        // assumes that all child routes are equivalent
        map((routePath: string) => routePath.slice(1).split('/')[1])
      );

    const termsOfAccessRoute = (enrolmentStatus === EnrolmentStatus.UNDER_REVIEW)
      ? EnrolmentRoutes.SUBMISSION_CONFIRMATION
      : (enrolmentStatus === EnrolmentStatus.REQUIRES_TOA)
        ? EnrolmentRoutes.PENDING_ACCESS_TERM
        : EnrolmentRoutes.CURRENT_ACCESS_TERM;

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
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            ),
            forceActive: currentRoutePath$
              .pipe(
                map((routePath: string) =>
                  EnrolmentRoutes.enrolmentProfileRoutes().includes(routePath)
                )
              )
          },
          {
            name: 'Terms of Access',
            icon: statusIcons.accessAgreement,
            route: termsOfAccessRoute,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            ),
            // TODO
            // forceActive: currentRoutePath$
            //   .pipe(
            //     map((routePath: string) =>
            //       [
            //         EnrolmentRoutes.SUBMISSION_CONFIRMATION
            //       ].includes(routePath)
            //     )
            //   )
          },
          {
            name: 'PharmaNet Enrolment Certificate',
            icon: statusIcons.certificate,
            route: EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
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
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'date_range',
            route: EnrolmentRoutes.PHARMANET_TRANSACTIONS,
            showItem: true,
            disabled: true, // (
            //   !hasAcceptedAtLeastOneToa ||
            //   [
            //     EnrolmentStatus.LOCKED
            //   ].includes(enrolmentStatus)
            // ),
            deemphasize: true // this.enrolmentService.isInitialEnrolment
          },
          {
            name: 'PRIME History',
            icon: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'history',
            route: EnrolmentRoutes.ACCESS_TERMS,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatus.LOCKED
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
    let certificate = 'card_membership';

    if (!hasAcceptedAtLeastOneToa) {
      // Default icons when performing initial enrolment
      accessAgreement = 'lock';
      certificate = 'lock';

      switch (enrolmentStatus) {
        case EnrolmentStatus.ACTIVE:
          break;
        case EnrolmentStatus.UNDER_REVIEW:
        case EnrolmentStatus.REQUIRES_TOA:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatus.LOCKED:
          enrollee = 'lock';
          break;
      }
    } else {
      switch (enrolmentStatus) {
        case EnrolmentStatus.ACTIVE:
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
