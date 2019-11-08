import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { ViewportService } from '@core/services/viewport.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';

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
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private viewportService: ViewportService,
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
    this.authService.logout(this.config.loginRedirectUrl);
  }

  public async ngOnInit() {
    this.sideNavSections = this.getSideNavSections();
    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);
    // Subscribe to viewport onresize changes
    this.viewportService.onResize()
      .subscribe((device: string) => this.setSideNavProps(device));

    const user = await this.authService.getUser();
    this.username = `${user.firstName} ${user.firstName}`;
  }

  private getSideNavSections() {
    return (this.authService.isProvisioner() || this.authService.isAdmin())
      ? this.getProvisionSideNavSections()
      : this.getEnrolleeSideNavSections();
  }

  private getEnrolleeSideNavSections() {
    this.enrolmentResource.enrolments()
      .subscribe(
        (enrolment: Enrolment) => {

        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be retrieved');
          this.logger.error('[Shared] Dashboard::getEnrolleeSideNavSections error has occurred: ', error);
        }
      );

    return [
      {
        header: 'Application Enrolment',
        showHeader: false,
        items: [
          {
            name: 'Enrolment',
            icon: 'assignment_ind', // assignment_turned_in
            route: '/enrolment/profile',
            showItem: true
          },
          {
            name: 'Access Agreement',
            icon: 'lock', // assignment, assignment_turned_in
            route: '/enrolment/agreement',
            showItem: true
          },
          {
            name: 'Status',
            icon: 'lock', // assignment_turned_in
            route: '/enrolment/summary',
            showItem: true
          }
        ]
      }
    ];
  }

  private getProvisionSideNavSections() {
    return [
      {
        header: 'Pharmacist Enrolments',
        showHeader: false,
        items: [
          {
            name: 'Enrolments',
            icon: 'format_list_bulleted',
            route: '/provision/enrolments',
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
