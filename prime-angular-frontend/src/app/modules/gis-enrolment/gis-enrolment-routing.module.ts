import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { GisEnrolmentRoutes } from './gis-enrolment.routes';
import { GisDashboardComponent } from './shared/components/gis-dashboard/gis-dashboard.component';
import { GisEnrolmentGuard } from './shared/guards/gis-enrolment.guard';

const routes: Routes = [
  {
    path: '',
    component: GisDashboardComponent,
    canLoad: [
      GisEnrolmentGuard
    ],
    canActivate: [],
    canActivateChild: [
      GisEnrolmentGuard
    ],
    children: [
      // TODO lazy loading every page on small enrolment looking for gotchas
      {
        path: GisEnrolmentRoutes.LDAP_USER_PAGE,
        loadChildren: () => import('./pages/ldap-user-page/ldap-user-page.module').then(m => m.LdapUserPageModule)
      },
      {
        path: GisEnrolmentRoutes.LDAP_INFO_PAGE,
        loadChildren: () => import('./pages/ldap-information-page/ldap-information-page.module').then(m => m.LdapInformationPageModule)
      },
      {
        path: GisEnrolmentRoutes.ORG_INFO_PAGE,
        loadChildren: () => import('./pages/organization-information-page/organization-information-page.module').then(m => m.OrganizationInformationPageModule)
      },
      {
        path: GisEnrolmentRoutes.ENROLLEE_INFO_PAGE,
        loadChildren: () => import('./pages/enrollee-information-page/enrollee-information-page.module').then(m => m.EnrolleeInformationPageModule)
      },
      {
        path: GisEnrolmentRoutes.SUBMISSION_CONFIRMATION,
        loadChildren: () => import('./pages/submission-confirmation-page/submission-confirmation-page.module').then(m => m.SubmissionConfirmationPageModule)
      },
      {
        path: '', // Equivalent to `/` and alias for default view
        redirectTo: GisEnrolmentRoutes.LDAP_USER_PAGE,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GisEnrolmentRoutingModule { }
