import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { AdjudicationRoutes } from './adjudication.routes';
import { AdjudicationGuard } from './shared/guards/adjudication.guard';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { EnrolleeProfileVersionsComponent } from './pages/enrollee-profile-versions/enrollee-profile-versions.component';
import { EnrolleeProfileVersionComponent } from './pages/enrollee-profile-version/enrollee-profile-version.component';
import { EnrolleeAccessTermsComponent } from './pages/enrollee-access-terms/enrollee-access-terms.component';
import { EnrolleeAccessTermComponent } from './pages/enrollee-access-term/enrollee-access-term.component';
import { EnrolleeAccessTermEnrolmentComponent } from './pages/enrollee-access-term-enrolment/enrollee-access-term-enrolment.component';
import { EnrolleeEventsComponent } from './pages/enrollee-events/enrollee-events.component';
import { EnrolleeReviewStatusComponent } from './pages/enrollee-review-status/enrollee-review-status.component';

const routes: Routes = [
  {
    path: '',
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
            component: EnrolmentsComponent
          },
          {
            path: ':id',
            children: [
              {
                path: '',
                component: EnrolmentComponent,
                data: { title: 'Enrollee' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_ADJUDICATOR_NOTES,
                component: AdjudicatorNotesComponent,
                data: { title: 'Adjudicator Notes' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_LIMITS_CONDITIONS,
                component: LimitsConditionsClausesComponent,
                data: { title: 'Limits and Conditions Clauses' }
              },
              {
                path: AdjudicationRoutes.ENROLLEE_TERMS_HISTORY,
                children: [
                  {
                    path: '',
                    component: EnrolleeAccessTermsComponent,
                    data: { title: 'Terms of Access History' }
                  },
                  {
                    path: ':hid',
                    children: [
                      {
                        path: '',
                        component: EnrolleeAccessTermComponent,
                        data: { title: 'Terms of Access' }
                      },
                      {
                        path: AdjudicationRoutes.ENROLLEE,
                        component: EnrolleeAccessTermEnrolmentComponent,
                        data: { title: 'Enrolment' }
                      },
                    ]
                  },
                ],
              },
              {
                path: AdjudicationRoutes.ENROLLEE_PROFILE_HISTORY,
                children: [
                  {
                    path: '',
                    component: EnrolleeProfileVersionsComponent,
                    data: { title: 'Enrolment Histories' }
                  },
                  {
                    path: ':hid',
                    component: EnrolleeProfileVersionComponent,
                    data: { title: 'Enrolment History' }
                  }
                ]
              },
              {
                path: AdjudicationRoutes.ENROLLEE_EVENTS,
                children: [
                  {
                    path: '',
                    component: EnrolleeEventsComponent,
                    data: { title: 'Enrolment Events' }
                  }
                ]
              },
              {
                path: AdjudicationRoutes.ENROLLEE_REVIEW_STATUS,
                children: [
                  {
                    path: '',
                    component: EnrolleeReviewStatusComponent,
                    data: { title: 'Enrolment Review Status' }
                  }
                ]
              }
            ]
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for `enrolments`
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
