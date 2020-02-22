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
        path: AdjudicationRoutes.ENROLMENTS,
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
                path: 'adjudicator-notes',
                component: AdjudicatorNotesComponent,
                data: { title: 'Adjudicator Notes' }
              },
              {
                path: 'limits-conditions-clauses',
                component: LimitsConditionsClausesComponent,
                data: { title: 'Limits and Conditions Clauses' }
              },
              {
                path: AdjudicationRoutes.ACCESS_TERMS,
                children: [
                  {
                    path: '',
                    component: EnrolleeAccessTermsComponent,
                    data: { title: 'Enrollee Access Terms' }
                  },
                  {
                    path: ':hid',
                    children: [
                      {
                        path: '',
                        component: EnrolleeAccessTermComponent,
                        data: { title: 'Enrollee Access Term' }
                      },
                      {
                        path: AdjudicationRoutes.ENROLMENT,
                        component: EnrolleeAccessTermEnrolmentComponent,
                        data: { title: 'Access Term Enrolment' }
                      },
                    ]
                  },
                ],
              },
              {
                path: 'history',
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
              }
            ]
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for `enrolments`
        redirectTo: AdjudicationRoutes.ENROLMENTS,
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
