import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { HealthAuthSiteRegRoutes } from './health-auth-site-reg.routes';
import { HealthAuthoritySiteGuard } from './shared/guards/health-authority-site-guard.service';
import { AuthorizedUserGuard } from './shared/guards/authorized-user.guard';
import { HealthAuthSiteRegDashboardComponent } from './shared/components/health-auth-site-reg-dashboard/health-auth-site-reg-dashboard.component';

import { CollectionNoticePageComponent } from '@health-auth/pages/collection-notice-page/collection-notice-page.component';
import { AuthorizedUserPageComponent } from '@health-auth/pages/authorized-user-page/authorized-user-page.component';
import { AuthorizedUserNextStepsPageComponent } from '@health-auth/pages/authorized-user-next-steps-page/authorized-user-next-steps-page.component';
import { AuthorizedUserApprovedPageComponent } from '@health-auth/pages/authorized-user-approved-page/authorized-user-approved-page.component';
import { AuthorizedUserDeclinedPageComponent } from '@health-auth/pages/authorized-user-declined-page/authorized-user-declined-page.component';
import { AuthorizedUserDisabledPageComponent } from './pages/authorized-user-disabled-page/authorized-user-disabled-page.component';
import { SiteManagementPageComponent } from '@health-auth/pages/site-management-page/site-management-page.component';
import { HealthAuthCareTypePageComponent } from '@health-auth/pages/health-auth-care-type-page/health-auth-care-type-page.component';
import { SiteInformationPageComponent } from '@health-auth/pages/site-information-page/site-information-page.component';
import { HoursOperationPageComponent } from '@health-auth/pages/hours-operation-page/hours-operation-page.component';
import { AdministratorPageComponent } from '@health-auth/pages/administrator-page/administrator-page.component';
import { OverviewPageComponent } from '@health-auth/pages/overview-page/overview-page.component';
import { HealthAuthorityResolver } from '@health-auth/shared/resolvers/health-authority.resolver';

const routes: Routes = [
  {
    path: '',
    component: HealthAuthSiteRegDashboardComponent,
    canActivate: [AuthenticationGuard],
    canActivateChild: [AuthenticationGuard],
    children: [
      {
        path: HealthAuthSiteRegRoutes.COLLECTION_NOTICE,
        component: CollectionNoticePageComponent,
        data: { title: 'Collection Notice' }
      },
      // Authorized user request for approval routes to gain
      // access to Health Authority site registration
      {
        path: HealthAuthSiteRegRoutes.ACCESS,
        canActivateChild: [AuthorizedUserGuard],
        children: [
          {
            path: HealthAuthSiteRegRoutes.ACCESS_AUTHORIZED_USER,
            component: AuthorizedUserPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Authorized User' }
          },
          {
            path: HealthAuthSiteRegRoutes.ACCESS_REQUESTED,
            component: AuthorizedUserNextStepsPageComponent,
            data: { title: 'Access Requested' }
          },
          {
            path: HealthAuthSiteRegRoutes.ACCESS_APPROVED,
            component: AuthorizedUserApprovedPageComponent,
            data: { title: 'Access Confirmed' }
          },
          {
            path: HealthAuthSiteRegRoutes.ACCESS_DECLINED,
            component: AuthorizedUserDeclinedPageComponent,
            data: { title: 'Access Declined' }
          },
          {
            path: HealthAuthSiteRegRoutes.ACCESS_DISABLED,
            component: AuthorizedUserDisabledPageComponent,
            data: { title: 'Authorized user is disabled' }
          },
          {
            path: '', // Equivalent to `/` and alias for default view
            redirectTo: HealthAuthSiteRegRoutes.ACCESS_AUTHORIZED_USER,
            pathMatch: 'full'
          }
        ]
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        component: SiteManagementPageComponent,
        canActivate: [AuthorizedUserGuard],
        data: { title: 'Site Management' }
      },
      // Viewing and editing route for an existing and
      // approved authorized user
      {
        path: `${HealthAuthSiteRegRoutes.AUTHORIZED_USER}/:auid`,
        component: AuthorizedUserPageComponent,
        canActivate: [AuthorizedUserGuard],
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Authorized User' }
      },
      // Site registration and maintenance routes for administration
      // of health authority information
      {
        path: `${HealthAuthSiteRegRoutes.HEALTH_AUTHORITIES}/:haid/${HealthAuthSiteRegRoutes.SITES}/:sid`,
        canActivate: [AuthorizedUserGuard],
        canActivateChild: [HealthAuthoritySiteGuard],
        resolve: {
          healthAuthority: HealthAuthorityResolver
        },
        children: [
          {
            path: HealthAuthSiteRegRoutes.SITE_INFORMATION,
            component: SiteInformationPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Site Details' }
          },
          {
            path: HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE,
            component: HealthAuthCareTypePageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Health Authority Care Type' }
          },
          {
            path: HealthAuthSiteRegRoutes.HOURS_OPERATION,
            component: HoursOperationPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Hours of Operation' }
          },
          {
            path: HealthAuthSiteRegRoutes.ADMINISTRATOR,
            component: AdministratorPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PharmaNet Administrator & Technical Support' }
          },
          {
            path: HealthAuthSiteRegRoutes.SITE_OVERVIEW,
            component: OverviewPageComponent,
            data: { title: 'Information Review' }
          },
          {
            path: '', // Equivalent to `/` and alias for default view
            redirectTo: HealthAuthSiteRegRoutes.SITE_OVERVIEW,
            pathMatch: 'full'
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for default view
        redirectTo: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HealthAuthSiteRegRoutingModule { }
