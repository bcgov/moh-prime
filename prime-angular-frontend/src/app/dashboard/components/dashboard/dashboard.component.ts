import { Component, OnInit, ViewChild, Inject, NgZone } from '@angular/core';
import { MatSidenav } from '@angular/material';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'src/app/app-config.module';

import { AuthTokenService } from 'src/app/core/services/auth-token.service';
import { LoggerService } from 'src/app/core/services/logger.service';
import { ViewportService } from 'src/app/core/services/viewport.service';
import { WindowRefService } from 'src/app/core/services/window-ref.service';

import { DeviceResolution } from 'src/app/shared/enums/device-resolutions.enum';

declare const gapi: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidenav', { static: true }) public sideNav: MatSidenav;

  public sideNavSections: {};
  public sideNavProps: {
    opened: boolean;
    mode: string;
    showText: boolean;
  };

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private viewportService: ViewportService,
    private tokenService: AuthTokenService,
    private logger: LoggerService,
    private windowRef: WindowRefService,
    private ngZone: NgZone
  ) { }

  public get isAdmin(): boolean {
    // TODO: don't do this if time permits
    // NOTE: using the path to indicate the user role as the requirements don't require
    // admins to authenticate, which would provide a authentication token and claim
    return this.windowRef.nativeWindow.location.href.indexOf('admin') > -1;
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

  public logout() {
    gapi.load('auth2', () => {
      gapi.auth2.init().then(() => {
        const auth2 = gapi.auth2.getAuthInstance();
        auth2.signOut().then(() => {
          this.ngZone.run(() => {
            this.tokenService.removeToken();
            this.router.navigate(['/login']);
          });
        });
      });
    });
  }

  public ngOnInit() {
    this.sideNavSections = this.getSideNavSections();
    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);
    // Subscribe to viewport onresize changes
    this.viewportService
      .onResize()
      .subscribe((device: string) => this.setSideNavProps(device));
  }

  private getSideNavSections() {
    return this.isAdmin
      ? this.getAdminSideNavSections()
      : this.getApplicantSideNavSections();
  }

  private getApplicantSideNavSections() {
    return [
      {
        header: 'Applicant',
        showHeader: false,
        items: [
          {
            name: 'Enrolment',
            icon: 'vpn_key',
            route: '/dashboard/applicant/enrolment',
            showItem: true
          },
          {
            name: 'In Progress',
            icon: 'highlight_off',
            route: '/dashboard/applicant/inprogress',
            showItem: false
          },
          {
            name: 'Complete',
            icon: 'highlight_off',
            route: '/dashboard/applicant/complete',
            showItem: false
          }
        ]
      },
      {
        header: 'Manage',
        showHeader: false,
        items: [
          {
            name: 'Profile',
            icon: 'person',
            route: '/dashboard/applicant/profile',
            showItem: false
          },
          {
            name: 'Change Password',
            icon: 'lock',
            route: '/dashboard/applicant/password',
            showItem: false
          }
        ]
      }
    ];
  }

  private getAdminSideNavSections() {
    return [
      {
        header: 'Admin',
        showHeader: false,
        items: [
          {
            name: 'Applicants',
            icon: 'assignment',
            route: '/dashboard/admin/applicants',
            showItem: true
          }
        ]
      },
      {
        header: 'Manage',
        showHeader: false,
        items: [
          {
            name: 'Profile',
            icon: 'person',
            route: '/dashboard/applicant/profile',
            showItem: false
          },
          {
            name: 'Change Password',
            icon: 'lock',
            route: '/dashboard/applicant/password',
            showItem: false
          }
        ]
      }
    ];
  }

  private setSideNavProps(device: string) {
    if (device === DeviceResolution.MOBILE) {
      this.sideNavProps = {
        opened: false,
        mode: 'over',
        showText: false
      };
    } else if (device === DeviceResolution.TABLET) {
      this.sideNavProps = {
        opened: true,
        mode: 'side',
        showText: false
      };
    } else {
      this.sideNavProps = {
        opened: true,
        mode: 'side',
        showText: true
      };
    }
  }
}
