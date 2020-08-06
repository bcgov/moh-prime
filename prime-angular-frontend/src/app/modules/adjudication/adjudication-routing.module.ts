import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { AdjudicationRoutes } from './adjudication.routes';
import { AdjudicationGuard } from './shared/guards/adjudication.guard';

import { EnrolleesComponent } from './pages/enrollees/enrollees.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { EnrolleeEnrolmentsComponent } from './pages/enrollee-enrolments/enrollee-enrolments.component';
import { EnrolleeAccessTermComponent } from './pages/enrollee-access-term/enrollee-access-term.component';
import { EnrolleeAccessTermEnrolmentComponent } from './pages/enrollee-access-term-enrolment/enrollee-access-term-enrolment.component';
import { EnrolleeEventsComponent } from './pages/enrollee-events/enrollee-events.component';
import { EnrolleeReviewStatusComponent } from './pages/enrollee-review-status/enrollee-review-status.component';
import { SiteRegistrationsComponent } from './pages/site-registrations/site-registrations.component';
import { SiteRegistrationComponent } from './pages/site-registration/site-registration.component';
import { SiteAdjudicationComponent } from './pages/site-adjudication/site-adjudication.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';

const routes: Routes = [
  {
    path: AdjudicationRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [
      AuthenticationGuard,
      AdjudicationGuard
    ],
    resolve: [ConfigResolver],
    children: [
      {
        path: AdjudicationRoutes.ENROLLEES,
        children: [
          {
            path: '',
            component: EnrolleesComponent,
            data: { title: 'PRIME Enrollees' }
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
                path: AdjudicationRoutes.ENROLLEE_LIMITS_CONDITIONS,
                component: LimitsConditionsClausesComponent,
                data: { title: 'Limits and Conditions Clauses' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_ADJUDICATOR_NOTES,
                component: AdjudicatorNotesComponent,
                data: { title: 'Adjudicator Notes' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_EVENT_LOG,
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
            path: ':sid',
            children: [
              {
                path: '',
                component: SiteRegistrationComponent,
                data: { title: 'Site Registration' }
              },
              {
                path: AdjudicationRoutes.SITE_ADJUDICATION,
                component: SiteAdjudicationComponent,
                data: { title: 'Site Adjudication' }
              },
              {
                path: AdjudicationRoutes.ORGANIZATION_INFORMATION,
                children: [
                  {
                    path: ':oid',
                    component: OrganizationInformationComponent,
                    data: { title: 'Site Adjudication' }
                  },
                ]
              }
            ]
          }
        ]
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
