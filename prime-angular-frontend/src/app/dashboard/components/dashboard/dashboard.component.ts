import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatSidenav } from '@angular/material';
import { Router } from '@angular/router';

import { APP_CONFIG, AppConfig } from 'src/app/app-config.module';

import { LoggerService } from '../../../core/services/logger.service';
import { ViewportService } from '../../../core/services/viewport.service';

import { DeviceResolution } from '../../../shared/enums/device-resolutions.enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  @ViewChild('sidenav', { static: true }) public sideNav: MatSidenav;

  public sideNavSections: {};
  public sideNavProps: {
    opened: boolean,
    mode: string,
    showText: boolean
  };

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private router: Router,
    private viewportService: ViewportService,
    private logger: LoggerService
  ) { }

  public get isApplicant(): boolean {
    return true;
  }

  public get isAdmin(): boolean {
    return false;
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

  }

  public ngOnInit() {
    this.sideNavSections = this.getSideNavSections();
    // Initialize the sidenav with properties based on current viewport
    this.setSideNavProps(this.viewportService.device);
    // Subscribe to viewport onresize changes
    this.viewportService.onResize()
      .subscribe((device: string) => this.setSideNavProps(device));
  }

  private getSideNavSections() {
    return (this.isAdmin)
      ? this.getAdminSideNavSections()
      : this.getMemberSideNavSections();
  }

  private getMemberSideNavSections() {
    return [
      {
        header: 'Applicant',
        showHeader: true,
        items: [
          {
            name: 'Enrollment',
            icon: 'vpn_key',
            route: '/dashboard/applicant/enrollment',
            showItem: true
          },
          {
            name: 'Applicants',
            icon: 'assignment',
            route: '/dashboard/admin/applicants',
            showItem: true
          },
          {
            name: 'In Progress',
            icon: 'highlight_off',
            route: '/dashboard/applicant/inprogress',
            showItem: true
          },
          {
            name: 'Complete',
            icon: 'highlight_off',
            route: '/dashboard/applicant/complete',
            showItem: true
          },
          {
            name: 'Denied',
            icon: 'highlight_off',
            route: '/dashboard/applicant/denied',
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
            route: '/dashboard/member/profile',
            showItem: false
          },
          {
            name: 'Change Password',
            icon: 'lock',
            route: '/dashboard/member/password',
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
            name: 'Enrollment',
            icon: 'vpn_key',
            route: '/dashboard/applicant/enrollment',
            showItem: true
          },
          {
            name: 'Alerts',
            icon: 'notifications_active',
            route: '/dashboard/admin/alerts',
            showItem: false
          },
          {
            name: 'Service Calls',
            icon: 'call',
            route: '/dashboard/admin/calls',
            showItem: false
          },
          {
            name: 'Members',
            icon: 'card_membership',
            route: '/dashboard/admin/members',
            showItem: false
          }
        ]
      },
      {
        header: 'Manage',
        showHeader: false,
        items: [
          {
            name: 'Operators',
            icon: 'directions_boat',
            route: '/dashboard/admin/operators',
            showItem: false
          },
          {
            name: 'Profile',
            icon: 'person',
            route: '/dashboard/admin/profile',
            showItem: false
          },
          {
            name: 'Change Password',
            icon: 'lock',
            route: '/dashboard/admin/password',
            showItem: true
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
