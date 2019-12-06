import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { LoggerService } from '@core/services/logger.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';

import { AuthRoutes } from '@auth/auth.routes';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidenav', { static: false }) public sideNav: MatSidenav;

  public sideNavSections: {};
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
    this.sideNavSections = this.getSideNavSections();
    if (this.authService.isEnrollee()) {
      // Listen for enrolment status changes to update the side navigation
      // based on user progression
      this.enrolmentService.enrolment$
        .subscribe(() => {
          this.sideNavSections = this.getSideNavSections();
        });
    }

    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);
    // Listen for viewport onresize changes
    this.viewportService.onResize()
      .subscribe((device: string) => this.setSideNavProps(device));

    const user = await this.authService.getUser();
    this.username = `${user.firstName} ${user.firstName}`;
  }

  private getSideNavSections() {
    return (this.authService.isAdjudicator() || this.authService.isAdmin())
      ? this.getAdjudicationSideNavSections()
      : this.getEnrolleeSideNavSections();
  }

  private getEnrolleeSideNavSections() {
    const statusCode = (this.enrolmentService.enrolment)
      ? this.enrolmentService.enrolment.currentStatus.status.code
      : EnrolmentStatus.IN_PROGRESS;
    const statusIcons = this.getEnrolmentStatusIcons(statusCode);

    return [
      {
        header: 'Application Enrolment',
        showHeader: false,
        items: [
          {
            name: 'Enrolment',
            icon: statusIcons.enrolment,
            route: EnrolmentRoutes.PROFILE,
            showItem: true,
            forceActive: (statusCode === EnrolmentStatus.IN_PROGRESS)
          },
          {
            name: 'Access Agreement',
            icon: statusIcons.accessAgreement,
            route: EnrolmentRoutes.ACCESS_AGREEMENT,
            showItem: true,
            forceActive: (statusCode === EnrolmentStatus.SUBMITTED)
          },
          {
            name: 'Status',
            icon: statusIcons.status,
            route: EnrolmentRoutes.SUMMARY,
            showItem: true
          }
        ]
      }
    ];
  }

  private getAdjudicationSideNavSections() {
    return [
      {
        header: 'Pharmacist Enrolments',
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

  private getEnrolmentStatusIcons(statusCode: number) {
    let enrolment = 'assignment_turned_in';
    let accessAgreement = 'lock';
    let status = 'lock';
    switch (statusCode) {
      case EnrolmentStatus.IN_PROGRESS:
        enrolment = 'assignment_ind';
        break;
      case EnrolmentStatus.SUBMITTED:
        accessAgreement = 'schedule';
        break;
      case EnrolmentStatus.ADJUDICATED_APPROVED:
        accessAgreement = 'assignment';
        break;
      // case EnrolmentStatus.DECLINED:
      case EnrolmentStatus.ACCEPTED_TOS:
        accessAgreement = 'assignment_turned_in';
        status = 'assignment_turned_in';
        break;
      // case EnrolmentStatus.DECLINED_TOS:
    }

    return { enrolment, accessAgreement, status };
  }
}
