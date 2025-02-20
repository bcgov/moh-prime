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
import { SearchHAFormComponent } from './shared/components/search-ha-form/search-ha-form.component';
import { ReviewStatusContentComponent } from './shared/components/review-status-content/review-status-content.component';
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
import { SiteRegistrationTabsComponent } from './shared/components/site-registration-tabs/site-registration-tabs.component';
import { HealthAuthOrgInfoPageComponent } from './pages/health-authorities/health-auth-org-info-page/health-auth-org-info-page.component';
import { ContextualEnrolmentConfirmationComponent } from './shared/components/contextual-enrolment-confirmation/contextual-enrolment-confirmation.component';
import { MaintenanceContainerComponent } from './shared/components/maintenance-container/maintenance-container.component';
import { NotificationEmailsContainerComponent } from './shared/components/notification-emails-container/notification-emails-container.component';
import { NotificationEmailViewComponent } from './shared/components/notification-email-view/notification-email-view.component';
import { EnrolleeMaintenancePageComponent } from './pages/enrollee-maintenance-page/enrollee-maintenance-page.component';
import { SiteMaintenancePageComponent } from './pages/site-maintenance-page/site-maintenance-page.component';
import { EmailNotificationListPageComponent } from './pages/email-notification-list-page/email-notification-list-page.component';
import { EmailNotificationViewPageComponent } from './pages/email-notification-view-page/email-notification-view-page.component';
import { EnrolleeToaMaintenanceListPageComponent } from './pages/enrollee-toa-maintenance-list-page/enrollee-toa-maintenance-list-page.component';
import { EnrolleeToaMaintenanceViewPageComponent } from './pages/enrollee-toa-maintenance-view-page/enrollee-toa-maintenance-view-page.component';

import { HealthAuthCareTypesPageComponent } from './pages/health-authorities/health-auth-care-types-page/health-auth-care-types-page.component';
import { VendorsPageComponent } from './pages/health-authorities/vendors-page/vendors-page.component';
import { PrivacyOfficePageComponent } from './pages/health-authorities/privacy-office-page/privacy-office-page.component';
import { TechnicalSupportsPageComponent } from './pages/health-authorities/technical-supports-page/technical-supports-page.component';
import { AdministratorsPageComponent } from './pages/health-authorities/administrators-page/administrators-page.component';
import { AuthorizedUsersPageComponent } from './pages/health-authorities/authorized-users-page/authorized-users-page.component';
import { AuthorizedUserPageComponent } from './pages/health-authorities/authorized-user-page/authorized-user-page.component';
import { HealthAuthorityTableComponent } from './shared/components/health-authority-table/health-authority-table.component';
import { HealthAuthAuthorizedUsersViewComponent } from './shared/components/health-auth-authorized-users-view/health-auth-authorized-users-view.component';
import { AuthorizedUserReviewComponent } from './shared/components/authorized-user-review/authorized-user-review.component';
import { BannerListViewComponent } from './shared/components/banner-list-view/banner-list-view.component';
import { EnrolleeBannerListPageComponent } from './pages/enrollee-banner-list-page/enrollee-banner-list-page.component';
import { SiteBannerListPageComponent } from './pages/site-banner-list-page/site-banner-list-page.component';
import { OrganizationToaMaintenanceListPageComponent } from './pages/organization-toa-maintenance-list-page/organization-toa-maintenance-list-page.component';
import { OrganizationToaMaintenanceViewPageComponent } from './pages/organization-toa-maintenance-view-page/organization-toa-maintenance-view-page.component';
import { LicenseClassesMaintenancePageComponent } from './pages/license-classes-maintenance-page/license-classes-maintenance-page.component';
import { PlrInfoComponent } from './shared/components/plr-info/plr-info.component';
import { HealthAuthoritySiteContainerComponent } from './shared/components/health-authority-site-container/health-authority-site-container.component';
import { SiteOverviewPageComponent } from './pages/health-authorities/site-overview-page/site-overview-page.component';
import { SiteEventsPageComponent } from './pages/health-authorities/site-events-page/site-events-page.component';
import { SiteNotesPageComponent } from './pages/health-authorities/site-notes-page/site-notes-page.component';
import { SiteDocumentsPageComponent } from './pages/health-authorities/site-documents-page/site-documents-page.component';
import { CommunitySiteSubmissionPageComponent } from './pages/community-site-submission-page/community-site-submission-page.component';
import { CommunitySiteSubmissionListPageComponent } from './pages/community-site-submission-list-page/community-site-submission-list-page.component';
import { HaSiteSubmissionListPageComponent } from './pages/health-authorities/ha-site-submission-list-page/ha-site-submission-list-page.component';
import { HaSiteSubmissionPageComponent } from './pages/health-authorities/ha-site-submission-page/ha-site-submission-page.component';
import { SiteSubmissionListComponent } from './shared/components/site-submission-list/site-submission-list.component';
import { SiteSubmissionComponent } from './shared/components/site-submission/site-submission.component';
import { AdminUsersContainerComponent } from './shared/components/admin-users-container/admin-users-container.component';
import { AdminUsersPageComponent } from './pages/admin-users-page/admin-users-page.component';
import { AdminUsersTableComponent } from './shared/components/admin-users-table/admin-users-table.component';
import { OrganizationsComponent } from './pages/organizations/organizations.component';
import { OrganizationTableComponent } from './shared/components/organization-table/organization-table.component';
import { OrganizationContainerComponent } from './shared/components/organization-container/organization-container.component';
import { OrganizationSitesComponent } from './pages/organization-sites/organization-sites.component';

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
    SearchHAFormComponent,
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
    SiteRegistrationTabsComponent,
    HealthAuthOrgInfoPageComponent,
    ContextualEnrolmentConfirmationComponent,
    MaintenanceContainerComponent,
    NotificationEmailsContainerComponent,
    NotificationEmailViewComponent,
    EnrolleeMaintenancePageComponent,
    SiteMaintenancePageComponent,
    EmailNotificationListPageComponent,
    EmailNotificationViewPageComponent,
    EnrolleeToaMaintenanceListPageComponent,
    EnrolleeToaMaintenanceViewPageComponent,
    HealthAuthCareTypesPageComponent,
    VendorsPageComponent,
    PrivacyOfficePageComponent,
    TechnicalSupportsPageComponent,
    AdministratorsPageComponent,
    AuthorizedUsersPageComponent,
    AuthorizedUserPageComponent,
    HealthAuthorityTableComponent,
    HealthAuthAuthorizedUsersViewComponent,
    AuthorizedUserReviewComponent,
    BannerListViewComponent,
    EnrolleeBannerListPageComponent,
    SiteBannerListPageComponent,
    OrganizationToaMaintenanceListPageComponent,
    OrganizationToaMaintenanceViewPageComponent,
    LicenseClassesMaintenancePageComponent,
    PlrInfoComponent,
    HealthAuthoritySiteContainerComponent,
    SiteOverviewPageComponent,
    SiteEventsPageComponent,
    SiteNotesPageComponent,
    SiteDocumentsPageComponent,
    CommunitySiteSubmissionPageComponent,
    CommunitySiteSubmissionListPageComponent,
    HaSiteSubmissionListPageComponent,
    HaSiteSubmissionPageComponent,
    SiteSubmissionComponent,
    SiteSubmissionListComponent,
    AdminUsersContainerComponent,
    AdminUsersPageComponent,
    AdminUsersTableComponent,
    OrganizationsComponent,
    OrganizationTableComponent,
    OrganizationContainerComponent,
    OrganizationSitesComponent,
  ],
  imports: [
    AdjudicationRoutingModule,
    SharedModule,
    DashboardModule,
    EditorModule
  ]
})
export class AdjudicationModule { }
