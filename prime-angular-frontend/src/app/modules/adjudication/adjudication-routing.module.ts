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
import { UserAgreementNotesComponent } from './pages/user-agreement-notes/user-agreement-notes.component';
import { EnrolmentCertificateNotesComponent } from './pages/enrolment-certificate-notes/enrolment-certificate-notes.component';
import { EnrolleeProfileHistoriesComponent } from './pages/enrollee-profile-histories/enrollee-profile-histories.component';
import { EnrolleeProfileHistoryComponent } from './pages/enrollee-profile-history/enrollee-profile-history.component';

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
                path: 'user-agreement-notes',
                component: UserAgreementNotesComponent,
                data: { title: 'User Agreement Notes' }
              },
              {
                path: 'enrolment-certificate-notes',
                component: EnrolmentCertificateNotesComponent,
                data: { title: 'Enrolment Certificate Notes' }
              },
              {
                path: 'history',
                children: [
                  {
                    path: '',
                    component: EnrolleeProfileHistoriesComponent,
                    data: { title: 'Enrolment Histories' }
                  },
                  {
                    path: ':hid',
                    component: EnrolleeProfileHistoryComponent,
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
