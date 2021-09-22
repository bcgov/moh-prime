import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { AdjudicationRoutes } from './adjudication.routes';
import { AdjudicationGuard } from './shared/guards/adjudication.guard';
import { AdjudicationDashboardComponent } from './shared/components/adjudication-dashboard/adjudication-dashboard.component';

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
import { SiteRemoteUsersComponent } from './pages/site-remote-users/site-remote-users.component';
import { MetabaseReportsComponent } from './pages/metabase-reports/metabase-reports.component';
import { EnrolleeAdjudicatorNotesComponent } from './pages/enrollee-adjudicator-notes/enrollee-adjudicator-notes.component';
import { SiteAdjudicatorNotesComponent } from './pages/site-adjudicator-notes/site-adjudicator-notes.component';
import { SiteAdjudicatorDocumentsComponent } from './pages/site-adjudicator-documents/site-adjudicator-documents.component';
import { EnrolleeAdjudicatorDocumentsComponent } from './pages/enrollee-adjudicator-documents/enrollee-adjudicator-documents.component';
import { SiteEventsComponent } from './pages/site-events/site-events.component';
import { EnrolleeOverviewComponent } from './pages/enrollee-overview/enrollee-overview.component';
import { SiteOverviewComponent } from './pages/site-overview/site-overview.component';
import { EnrolleeBannerPageComponent } from './pages/enrollee-banner-page/enrollee-banner-page.component';
import { SiteBannerPageComponent } from './pages/site-banner-page/site-banner-page.component';
import { HealthAuthOrgInfoPageComponent } from './pages/health-authorities/health-auth-org-info-page/health-auth-org-info-page.component';
import { SiteMaintenancePageComponent } from './pages/site-maintenance-page/site-maintenance-page.component';
import { EnrolleeMaintenancePageComponent } from './pages/enrollee-maintenance-page/enrollee-maintenance-page.component';
import { EmailNotificationListPageComponent } from './pages/email-notification-list-page/email-notification-list-page.component';
import { EmailNotificationViewPageComponent } from './pages/email-notification-view-page/email-notification-view-page.component';
import { EnrolleeToaMaintenanceViewPageComponent } from './pages/enrollee-toa-maintenance-view-page/enrollee-toa-maintenance-view-page.component';
import { EnrolleeToaMaintenanceListPageComponent } from './pages/enrollee-toa-maintenance-list-page/enrollee-toa-maintenance-list-page.component';

import { HealthAuthCareTypesPageComponent } from './pages/health-authorities/health-auth-care-types-page/health-auth-care-types-page.component';
import { VendorsPageComponent } from './pages/health-authorities/vendors-page/vendors-page.component';
import { PrivacyOfficePageComponent } from './pages/health-authorities/privacy-office-page/privacy-office-page.component';
import { TechnicalSupportsPageComponent } from './pages/health-authorities/technical-supports-page/technical-supports-page.component';
import { AdministratorsPageComponent } from './pages/health-authorities/administrators-page/administrators-page.component';
import { AuthorizedUsersPageComponent } from './pages/health-authorities/authorized-users-page/authorized-users-page.component';
import { AuthorizedUserPageComponent } from './pages/health-authorities/authorized-user-page/authorized-user-page.component';
import { EnrolleeBannerListPageComponent } from './pages/enrollee-banner-list-page/enrollee-banner-list-page.component';
import { SiteBannerListPageComponent } from './pages/site-banner-list-page/site-banner-list-page.component';
import { OrganizationToaMaintenanceListPageComponent } from './pages/organization-toa-maintenance-list-page/organization-toa-maintenance-list-page.component';
import { OrganizationToaMaintenanceViewPageComponent } from './pages/organization-toa-maintenance-view-page/organization-toa-maintenance-view-page.component';
import {
  LicenseClassesMaintenancePageComponent
} from './pages/license-classes-maintenance-page/license-classes-maintenance-page.component';
import { SiteOverviewPageComponent } from './pages/health-authorities/site-overview-page/site-overview-page.component';
import { SiteInformationPageComponent } from './pages/health-authorities/site-information-page/site-information-page.component';
import { SiteEventLogPageComponent } from './pages/health-authorities/site-event-log-page/site-event-log-page.component';

const routes: Routes = [
  {
    path: '',
    component: AdjudicationDashboardComponent,
    canActivate: [
      AuthenticationGuard,
      AdjudicationGuard
    ],
    canActivateChild: [
      AuthenticationGuard
    ],
    children: [
      {
        path: AdjudicationRoutes.ENROLLEES,
        children: [
          {
            path: '',
            component: EnrolleesComponent,
            data: { title: 'Enrollees' }
          },
          {
            path: AdjudicationRoutes.BANNERS,
            children: [
              {
                path: '',
                component: EnrolleeBannerListPageComponent,
                data: { title: 'Enrollee Banners' }
              },
              {
                path: ':bid',
                component: EnrolleeBannerPageComponent,
                data: { title: 'Enrollee Banners' }
              }
            ]
          },
          {
            path: AdjudicationRoutes.MAINTENANCE,
            children: [
              {
                path: '',
                component: EnrolleeMaintenancePageComponent,
                data: { title: 'Enrollee Maintenance' }
              },
              {
                path: AdjudicationRoutes.NOTIFICATION_EMAILS,
                children: [
                  {
                    path: '',
                    component: EmailNotificationListPageComponent,
                    data: { title: 'Enrollee Notification Emails' }
                  },
                  {
                    path: ':eid',
                    component: EmailNotificationViewPageComponent,
                    data: { title: 'Enrollee Notification Email' }
                  },
                ]
              },
              {
                path: AdjudicationRoutes.TOA,
                children: [
                  {
                    path: '',
                    component: EnrolleeToaMaintenanceListPageComponent,
                    data: { title: 'Enrollee TOA Maintenance' }
                  },
                  {
                    path: ':aid',
                    component: EnrolleeToaMaintenanceViewPageComponent,
                    data: { title: 'Enrollee TOA Maintenance' }
                  },
                ]
              },
              {
                path: AdjudicationRoutes.LICENSE_CLASSES,
                component: LicenseClassesMaintenancePageComponent,
                data: { title: 'Licence Classes' }
              },
            ]
          },
          {
            path: ':id',
            children: [
              {
                path: AdjudicationRoutes.ENROLLEE_ENROLMENTS,
                children: [
                  {
                    path: '',
                    component: EnrolleeEnrolmentsComponent,
                    data: { title: 'Enrolments' }
                  },
                  {
                    path: AdjudicationRoutes.ENROLLEE_CURRENT_ENROLMENT,
                    component: EnrolmentComponent,
                    data: { title: 'Enrolment' }
                  },
                  {
                    path: ':aid',
                    children: [
                      {
                        path: AdjudicationRoutes.ENROLLEE_ACCESS_TERM,
                        component: EnrolleeAccessTermComponent,
                        data: { title: 'Enrolment Profile' }
                      },
                      {
                        path: AdjudicationRoutes.ENROLLEE_ACCESS_TERM_ENROLMENT,
                        component: EnrolleeAccessTermEnrolmentComponent,
                        data: { title: 'Enrolment Terms of Access' }
                      }
                    ]
                  }
                ]
              },
              {
                path: AdjudicationRoutes.ENROLLEE_REVIEW,
                component: EnrolleeReviewStatusComponent,
                data: { title: 'Adjudication' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_OVERVIEW,
                component: EnrolleeOverviewComponent,
                data: { title: 'Overview' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_LIMITS_CONDITIONS,
                component: LimitsConditionsClausesComponent,
                data: { title: 'Limits and Conditions Clauses' }
              },
              {
                path: AdjudicationRoutes.ADJUDICATOR_NOTES,
                component: EnrolleeAdjudicatorNotesComponent,
                data: { title: 'Adjudicator Notes' }
              },
              {
                path: AdjudicationRoutes.DOCUMENT_UPLOAD,
                component: EnrolleeAdjudicatorDocumentsComponent,
                data: { title: 'Document Upload' }
              },
              {
                path: AdjudicationRoutes.EVENT_LOG,
                component: EnrolleeEventsComponent,
                data: { title: 'Event Log' }
              }
            ]
          }
        ]
      },
      {
        path: AdjudicationRoutes.SITE_REGISTRATIONS,
        children: [
          {
            path: '',
            component: SiteRegistrationsComponent,
            data: { title: 'Site Registrations' }
          },
          {
            path: AdjudicationRoutes.BANNERS,
            children: [
              {
                path: '',
                component: SiteBannerListPageComponent,
                data: { title: 'Site Banners' }
              },
              {
                path: ':bid',
                component: SiteBannerPageComponent,
                data: { title: 'Site Banners' }
              }
            ]
          },
          {
            path: AdjudicationRoutes.MAINTENANCE,
            children: [
              {
                path: '',
                component: SiteMaintenancePageComponent,
                data: { title: 'Site Maintenance' }
              },
              {
                path: AdjudicationRoutes.NOTIFICATION_EMAILS,
                children: [
                  {
                    path: '',
                    component: EmailNotificationListPageComponent,
                    data: { title: 'Site Notification Emails' }
                  },
                  {
                    path: ':eid',
                    component: EmailNotificationViewPageComponent,
                    data: { title: 'Site Notification Email' }
                  },
                ]
              },
              {
                path: AdjudicationRoutes.TOA,
                children: [
                  {
                    path: '',
                    component: OrganizationToaMaintenanceListPageComponent,
                    data: { title: 'Organization Agreement Maintenance' }
                  },
                  {
                    path: ':aid',
                    component: OrganizationToaMaintenanceViewPageComponent,
                    data: { title: 'Organization Agreement Maintenance' }
                  },
                ]
              }
            ]
          },
          {
            // Site registrations is synonymous with organization with regards
            // to Community Pharmacy and Practice
            path: `:oid/${AdjudicationRoutes.SITE_REGISTRATION}/:sid`,
            children: [
              {
                path: '',
                component: SiteOverviewComponent,
                data: { title: 'Site Overview' }
              },
              {
                path: AdjudicationRoutes.SITE_INFORMATION,
                component: SiteInformationComponent,
                data: { title: 'Site Information' }
              },
              {
                path: AdjudicationRoutes.ORGANIZATION_INFORMATION,
                component: OrganizationInformationComponent,
                data: { title: 'Organization Information' }
              },
              {
                path: AdjudicationRoutes.SITE_REMOTE_USERS,
                component: SiteRemoteUsersComponent,
                data: { title: 'Remote Practitioners' }
              },
              {
                path: AdjudicationRoutes.ADJUDICATOR_NOTES,
                component: SiteAdjudicatorNotesComponent,
                data: { title: 'Adjudicator Notes' }
              },
              {
                path: AdjudicationRoutes.DOCUMENT_UPLOAD,
                component: SiteAdjudicatorDocumentsComponent,
                data: { title: 'Document Upload' }
              },
              {
                path: AdjudicationRoutes.EVENT_LOG,
                component: SiteEventsComponent,
                data: { title: 'Event Log' }
              }
            ]
          },
          {
            // TODO split out from under site registration into own module and lazy load, but
            //      requires reworking of the page specific routing to use absolute paths
            path: `${AdjudicationRoutes.HEALTH_AUTHORITIES}/:haid`,
            children: [
              {
                path: AdjudicationRoutes.ORGANIZATION_INFORMATION,
                component: HealthAuthOrgInfoPageComponent,
                data: { title: 'Organization Information' }
              },
              {
                path: AdjudicationRoutes.HEALTH_AUTH_CARE_TYPES,
                component: HealthAuthCareTypesPageComponent,
                data: { title: 'Health Authority Care Types' }
              },
              {
                path: AdjudicationRoutes.HEALTH_AUTH_VENDORS,
                component: VendorsPageComponent,
                data: { title: 'Vendors' }
              },
              {
                path: AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICE,
                component: PrivacyOfficePageComponent,
                data: { title: 'Privacy Office' }
              },
              {
                path: AdjudicationRoutes.HEALTH_AUTH_TECHNICAL_SUPPORTS,
                component: TechnicalSupportsPageComponent,
                data: { title: 'Technical Support(s)' }
              },
              {
                path: AdjudicationRoutes.HEALTH_AUTH_ADMINISTRATORS,
                component: AdministratorsPageComponent,
                data: { title: 'PharmaNet Administrator(s)' }
              },
              {
                path: AdjudicationRoutes.HEALTH_AUTH_AUTHORIZED_USERS,
                children: [
                  {
                    path: '',
                    component: AuthorizedUsersPageComponent,
                    data: { title: 'Authorized Users' }
                  },
                  // TODO combine the last two routes into a single route passing /0 for create users
                  // {
                  //   path: `${ AdjudicationRoutes.HEALTH_AUTH_AUTHORIZED_USER }/:auid`,
                  //   component: AuthorizedUserPageComponent,
                  //   data: { title: 'Authorized User' }
                  // }
                  {
                    path: AdjudicationRoutes.HEALTH_AUTH_AUTHORIZED_USER,
                    component: AuthorizedUserPageComponent,
                    data: { title: 'Authorized User' }
                  },
                  {
                    path: `:auid`,
                    component: AuthorizedUserPageComponent,
                    data: { title: 'Authorized User' }
                  }
                ]
              },
              {
                // Site registrations is synonymous with site with regards
                // to Health Authorities
                path: `${AdjudicationRoutes.SITE_REGISTRATION}/:sid`,
                children: [
                  {
                    path: '',
                    component: SiteOverviewPageComponent,
                    data: { title: 'Site Overview' }
                  },
                  {
                    path: AdjudicationRoutes.SITE_INFORMATION,
                    component: SiteInformationPageComponent,
                    data: { title: 'Site Information' }
                  },
                  {
                    path: AdjudicationRoutes.EVENT_LOG,
                    component: SiteEventLogPageComponent,
                    data: { title: 'Event Log' }
                  },
                  {
                    path: AdjudicationRoutes.ADJUDICATOR_NOTES,
                    // TODO replace with health authority site equivalent page component
                    component: SiteAdjudicatorNotesComponent,
                    data: { title: 'Adjudicator Notes' }
                  }
                ]
              },
              {
                path: '',
                redirectTo: AdjudicationRoutes.ORGANIZATION_INFORMATION,
                pathMatch: 'full'
              }
            ]
          }
        ]
      },
      {
        path: AdjudicationRoutes.METABASE_REPORTS,
        component: MetabaseReportsComponent,
        data: { title: 'Metabase Reports' }
      },
      {
        path: '', // Equivalent to `/` and alias for `enrollees`
        redirectTo: AdjudicationRoutes.ENROLLEES,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdjudicationRoutingModule { }
