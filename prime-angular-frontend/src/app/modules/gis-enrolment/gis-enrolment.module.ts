import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { GisEnrolmentRoutingModule } from './gis-enrolment-routing.module';
import { GisDashboardComponent } from './shared/components/gis-dashboard/gis-dashboard.component';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { LdapUserPageComponent } from './pages/ldap-user-page/ldap-user-page.component';
import { LdapInformationPageComponent } from './pages/ldap-information-page/ldap-information-page.component';
import { EnrolleeInformationPageComponent } from './pages/enrollee-information-page/enrollee-information-page.component';
import { SubmissionConfirmationPageComponent } from './pages/submission-confirmation-page/submission-confirmation-page.component';
import { GisEnrolmentProgressIndicatorComponent } from './shared/components/gis-enrolment-progress-indicator/gis-enrolment-progress-indicator.component';

@NgModule({
  declarations: [
    GisDashboardComponent,
    CollectionNoticePageComponent,
    LdapUserPageComponent,
    LdapInformationPageComponent,
    EnrolleeInformationPageComponent,
    SubmissionConfirmationPageComponent,
    GisEnrolmentProgressIndicatorComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    GisEnrolmentRoutingModule
  ]
})
export class GisEnrolmentModule { }
