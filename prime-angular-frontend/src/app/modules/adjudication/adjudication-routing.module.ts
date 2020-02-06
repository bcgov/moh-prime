import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
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

const routes: Routes = [
  {
    path: AdjudicationRoutes.MODULE_PATH,
    component: DashboardComponent,
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
