import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { GisEnrolmentRoutes } from './gis-enrolment.routes';
import { GisEnrolmentGuard } from './shared/guards/gis-enrolment.guard';
import { GisDashboardComponent } from './shared/components/gis-dashboard/gis-dashboard.component';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { LdapUserPageComponent } from './pages/ldap-user-page/ldap-user-page.component';
import { LdapInformationPageComponent } from './pages/ldap-information-page/ldap-information-page.component';
import { OrganizationInformationPageComponent } from './pages/organization-information-page/organization-information-page.component';
import { EnrolleeInformationPageComponent } from './pages/enrollee-information-page/enrollee-information-page.component';
import { SubmissionConfirmationPageComponent } from './pages/submission-confirmation-page/submission-confirmation-page.component';

const routes: Routes = [
  {
    path: '',
    component: GisDashboardComponent,
    canActivate: [
      AuthenticationGuard
    ],
    canActivateChild: [
      AuthenticationGuard,
      GisEnrolmentGuard
    ],
    children: [
      {
        path: GisEnrolmentRoutes.COLLECTION_NOTICE,
        component: CollectionNoticePageComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: GisEnrolmentRoutes.LDAP_USER_PAGE,
        component: LdapUserPageComponent,
        data: { title: 'GIS Enrolment' }
      },
      {
        path: GisEnrolmentRoutes.LDAP_INFO_PAGE,
        component: LdapInformationPageComponent,
        data: { title: 'GIS Enrolment' }
      },
      {
        path: GisEnrolmentRoutes.ORG_INFO_PAGE,
        component: OrganizationInformationPageComponent,
        data: { title: 'GIS Enrolment' }
      },
      {
        path: GisEnrolmentRoutes.ENROLLEE_INFO_PAGE,
        component: EnrolleeInformationPageComponent,
        data: { title: 'GIS Enrolment' }
      },
      {
        path: GisEnrolmentRoutes.SUBMISSION_CONFIRMATION,
        component: SubmissionConfirmationPageComponent,
        data: { title: 'GIS Enrolment' }
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
