import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';

import { merge } from 'rxjs';
import { map, distinctUntilChanged } from 'rxjs/operators';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { RouteStateService } from '@core/services/route-state.service';
import { ViewportService } from '@core/services/viewport.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';


interface DashboardNavSectionV1 {
  items: DashboardNavSectionItemV1[];
}

interface DashboardNavSectionItemV1 {
  name: string;
  route: string | (string | number)[];
  icon?: string;
  showItem?: boolean;
  disabled?: boolean;
  deemphasize?: boolean; // Reduce opacity
  forceActive?: boolean;
}

@Component({
  selector: 'app-dashboardv1',
  templateUrl: './dashboardv1.component.html',
  styleUrls: ['./dashboardv1.component.scss']
})
export class DashboardV1Component implements OnInit {
  @ViewChild('sidenav') public sideNav: MatSidenav;

  public dashboardNavSections: DashboardNavSectionV1[];
  public sideNavProps: {
    mode: string,
    opened: boolean,
    fixedInViewport: boolean,
    showText: boolean;
  };
  public username: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private route: ActivatedRoute,
    private router: Router,
    private routeStateService: RouteStateService,
    private authService: AuthService,
    private permissionService: PermissionService,
    private viewportService: ViewportService,
    private enrolmentService: EnrolmentService,
    private logger: ConsoleLoggerService
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
    // Dashboard is only used for Enrolments now, and will eventually
    // be replaced, but logout needs to force auth route now with
    // the existence of PHSA
    let routePath = EnrolmentRoutes.BCSC_LOGIN;

    if (this.permissionService.hasRoles(Role.ADMIN)) {
      routePath = `${routePath}/${AdjudicationRoutes.LOGIN_PAGE}`;
    }

    this.authService.logout(routePath);
  }

  public async ngOnInit() {
    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);

    // Initialize the side navigation based on the type of user
    this.dashboardNavSections = this.getSideNavSections();

    if (this.permissionService.hasRoles(Role.ENROLLEE)) {
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
    // Identity providers don't all provide last name
    this.username = `${user?.firstName} ${user.lastName ?? ''}`;
  }

  private getSideNavSections(): DashboardNavSectionV1[] {
    return this.getEnrolleeSideNavSections();
  }

  private getEnrolleeSideNavSections(): DashboardNavSectionV1[] {
    const enrolment = this.enrolmentService.enrolment;
    const enrolmentStatus = (enrolment)
      ? enrolment.currentStatus.statusCode
      : EnrolmentStatusEnum.EDITABLE;
    // Check if the enrollee is within their initial enrolment
    const hasAcceptedAtLeastOneToa = (enrolment)
      ? !!enrolment.expiryDate
      : false;
    const statusIcons = this.getEnrolmentStatusIcons(enrolmentStatus, hasAcceptedAtLeastOneToa);
    const currentRoute = this.router.url.slice(1).split('/')[1];

    const termsOfAccessRoute = (enrolmentStatus === EnrolmentStatusEnum.UNDER_REVIEW)
      ? EnrolmentRoutes.SUBMISSION_CONFIRMATION
      : (enrolmentStatus === EnrolmentStatusEnum.REQUIRES_TOA)
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
                EnrolmentStatusEnum.LOCKED,
                EnrolmentStatusEnum.DECLINED
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
                EnrolmentStatusEnum.LOCKED,
                EnrolmentStatusEnum.DECLINED
              ].includes(enrolmentStatus)
            ),
            forceActive: [
              EnrolmentRoutes.PENDING_ACCESS_TERM,
              EnrolmentRoutes.CURRENT_ACCESS_TERM
            ].includes(currentRoute)
          },
          {
            name: 'Next Steps to Get PharmaNet',
            icon: statusIcons.certificate,
            route: EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatusEnum.LOCKED,
                EnrolmentStatusEnum.DECLINED
              ].includes(enrolmentStatus)
            )
          }
        ]
      },
      {
        items: [
          {
            name: 'PRIME History',
            icon: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatusEnum.LOCKED,
                EnrolmentStatusEnum.DECLINED
              ].includes(enrolmentStatus)
            )
              ? 'lock'
              : 'history',
            route: EnrolmentRoutes.ACCESS_TERMS,
            showItem: true,
            disabled: (
              !hasAcceptedAtLeastOneToa ||
              [
                EnrolmentStatusEnum.LOCKED,
                EnrolmentStatusEnum.DECLINED
              ].includes(enrolmentStatus)
            ),
            deemphasize: this.enrolmentService.isInitialEnrolment
          }
        ]
      },
    ];
  }

  private getEnrolmentStatusIcons(enrolmentStatus: EnrolmentStatusEnum, hasAcceptedAtLeastOneToa: boolean) {
    let enrollee = 'assignment_ind';
    let accessAgreement = 'assignment';
    let certificate = 'mail';

    if (!hasAcceptedAtLeastOneToa) {
      // Default icons when performing initial enrolment
      accessAgreement = 'lock';
      certificate = 'lock';

      switch (enrolmentStatus) {
        case EnrolmentStatusEnum.EDITABLE:
          break;
        case EnrolmentStatusEnum.UNDER_REVIEW:
        case EnrolmentStatusEnum.REQUIRES_TOA:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatusEnum.LOCKED:
          enrollee = 'lock';
          break;
        case EnrolmentStatusEnum.DECLINED:
          enrollee = 'lock';
          accessAgreement = 'lock';
          certificate = 'lock';
          break;
      }
    } else {
      switch (enrolmentStatus) {
        case EnrolmentStatusEnum.EDITABLE:
          break;
        case EnrolmentStatusEnum.UNDER_REVIEW:
        case EnrolmentStatusEnum.REQUIRES_TOA:
          accessAgreement = 'schedule';
          break;
        case EnrolmentStatusEnum.LOCKED:
        case EnrolmentStatusEnum.DECLINED:
          enrollee = 'lock';
          accessAgreement = 'lock';
          certificate = 'lock';
          break;
      }

    }

    return { enrollee, accessAgreement, certificate };
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
