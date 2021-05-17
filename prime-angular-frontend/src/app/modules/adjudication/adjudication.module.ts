import { NgModule } from '@angular/core';

import { EditorModule } from '@tinymce/tinymce-angular';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { AdjudicationRoutingModule } from './adjudication-routing.module';

import { AdjudicationContainerComponent } from './shared/components/adjudication-container/adjudication-container.component';
import { AdjudicatorActionsComponent } from './shared/components/adjudicator-actions/adjudicator-actions.component';
import { EnrolleesComponent } from './pages/enrollees/enrollees.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { EnrolleeEnrolmentsComponent } from './pages/enrollee-enrolments/enrollee-enrolments.component';
import { EnrolleeAccessTermComponent } from './pages/enrollee-access-term/enrollee-access-term.component';
import { EnrolleeAccessTermEnrolmentComponent } from './pages/enrollee-access-term-enrolment/enrollee-access-term-enrolment.component';
import { EnrolleeEventsComponent } from './pages/enrollee-events/enrollee-events.component';
import { EnrolleeReviewStatusComponent } from './pages/enrollee-review-status/enrollee-review-status.component';
import { SiteRegistrationsComponent } from './pages/site-registrations/site-registrations.component';
import { SiteInformationComponent } from './pages/site-information/site-information.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';
import { AdjudicationDashboardComponent } from './shared/components/adjudication-dashboard/adjudication-dashboard.component';
import { DatedContentTableComponent } from './shared/components/dated-content-table/dated-content-table.component';
import { EnrolleeTableComponent } from './shared/components/enrollee-table/enrollee-table.component';
import { SearchFormComponent } from './shared/components/search-form/search-form.component';
import {
  ReviewStatusContentComponent
} from './shared/components/review-status-content/review-status-content.component';
import { SiteRegistrationContainerComponent } from './shared/components/site-registration-container/site-registration-container.component';
import { SiteRegistrationTableComponent } from './shared/components/site-registration-table/site-registration-table.component';
import { SiteRegistrationActionsComponent } from './shared/components/site-registration-actions/site-registration-actions.component';
import { SiteRemoteUsersComponent } from './pages/site-remote-users/site-remote-users.component';
import { MetabaseReportsComponent } from './pages/metabase-reports/metabase-reports.component';
import { EnrolleeAdjudicatorNotesComponent } from './pages/enrollee-adjudicator-notes/enrollee-adjudicator-notes.component';
import { SiteAdjudicatorNotesComponent } from './pages/site-adjudicator-notes/site-adjudicator-notes.component';
import { AdjudicatorNotesComponent } from './shared/components/adjudicator-notes/adjudicator-notes.component';
import { SiteAdjudicatorDocumentsComponent } from './pages/site-adjudicator-documents/site-adjudicator-documents.component';
import { EnrolleeAdjudicatorDocumentsComponent } from './pages/enrollee-adjudicator-documents/enrollee-adjudicator-documents.component';
import { AdjudicatorDocumentsComponent } from './shared/components/adjudicator-documents/adjudicator-documents.component';
import { SiteEventsComponent } from './pages/site-events/site-events.component';
import { AdjudicatorEventsComponent } from './shared/components/adjudicator-events/adjudicator-events.component';
import { EnrolleeOverviewComponent } from './pages/enrollee-overview/enrollee-overview.component';
import { SiteOverviewComponent } from './pages/site-overview/site-overview.component';
import { ContextualEnrolleeNotificationComponent } from './shared/components/contextual-enrollee-notification/contextual-enrollee-notification.component';
import { ContextualSiteNotificationComponent } from './shared/components/contextual-site-notification/contextual-site-notification.component';
import { EnrolleeBannerPageComponent } from './pages/enrollee-banner-page/enrollee-banner-page.component';
import { SiteBannerPageComponent } from './pages/site-banner-page/site-banner-page.component';
import { BannerMaintenanceComponent } from './shared/components/banner-maintenance/banner-maintenance.component';
import { HealthAuthorityTableComponent } from './shared/components/health-authority-table/health-authority-table.component';
import { SiteRegistrationTabsComponent } from './shared/components/site-registration-tabs/site-registration-tabs.component';
import { HealthAuthorityAuthorizedUserPageComponent } from './pages/health-authority-authorized-user-page/health-authority-authorized-user-page.component';
import { HaAuthorizedUsersViewComponent } from './shared/components/ha-authorized-users-view/ha-authorized-users-view.component';
import { HealthAuthorityAuthorizedUsersPageComponent } from './pages/health-authority-authorized-users-page/health-authority-authorized-users-page.component';
import { AuthorizedUserReviewComponent } from './shared/components/authorized-user-review/authorized-user-review.component';
import { ContextualEnrolmentConfirmationComponent } from './shared/components/contextual-enrolment-confirmation/contextual-enrolment-confirmation.component';

@NgModule({
  declarations: [
    AdjudicationContainerComponent,
    AdjudicatorActionsComponent,
    AdjudicatorNotesComponent,
    EnrolleesComponent,
    EnrolmentComponent,
    LimitsConditionsClausesComponent,
    EnrolleeEventsComponent,
    EnrolleeReviewStatusComponent,
    EnrolleeEnrolmentsComponent,
    EnrolleeAccessTermComponent,
    EnrolleeAccessTermEnrolmentComponent,
    EnrolleeTableComponent,
    SearchFormComponent,
    DatedContentTableComponent,
    ReviewStatusContentComponent,
    SiteRegistrationsComponent,
    SiteInformationComponent,
    SiteRegistrationContainerComponent,
    SiteRegistrationTableComponent,
    SiteRegistrationActionsComponent,
    OrganizationInformationComponent,
    SiteRemoteUsersComponent,
    AdjudicationDashboardComponent,
    MetabaseReportsComponent,
    EnrolleeAdjudicatorNotesComponent,
    SiteAdjudicatorNotesComponent,
    SiteAdjudicatorDocumentsComponent,
    EnrolleeAdjudicatorDocumentsComponent,
    AdjudicatorDocumentsComponent,
    SiteEventsComponent,
    AdjudicatorEventsComponent,
    EnrolleeOverviewComponent,
    SiteOverviewComponent,
    ContextualEnrolleeNotificationComponent,
    ContextualSiteNotificationComponent,
    EnrolleeBannerPageComponent,
    SiteBannerPageComponent,
    BannerMaintenanceComponent,
    HealthAuthorityTableComponent,
    SiteRegistrationTabsComponent,
    HealthAuthorityAuthorizedUserPageComponent,
    HaAuthorizedUsersViewComponent,
    HealthAuthorityAuthorizedUsersPageComponent,
    AuthorizedUserReviewComponent,
    ContextualEnrolmentConfirmationComponent
  ],
  imports: [
    AdjudicationRoutingModule,
    SharedModule,
    DashboardModule,
    EditorModule
  ]
})
export class AdjudicationModule { }
