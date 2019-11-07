import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { EnrolleeGuard } from './shared/guards/enrollee.guard';
import { EnrolmentGuard } from './shared/guards/enrolment.guard';

import { ProfileComponent } from './pages/profile/profile.component';
import { ProfessionalInfoComponent } from './pages/professional-info/professional-info.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { PharmanetAccessComponent } from './pages/pharmanet-access/pharmanet-access.component';
import { ReviewComponent } from './pages/review/review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { SummaryComponent } from './pages/summary/summary.component';

const routes: Routes = [
  {
    path: 'enrolment',
    component: DashboardComponent,
    canActivateChild: [
      AuthenticationGuard,
      EnrolleeGuard,
      EnrolmentGuard
    ],
    resolve: [ConfigResolver],
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'professional',
        component: ProfessionalInfoComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'declaration',
        component: SelfDeclarationComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'access',
        component: PharmanetAccessComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'review',
        component: ReviewComponent
      },
      {
        path: 'confirmation',
        component: ConfirmationComponent
      },
      {
        path: 'agreement',
        component: AccessAgreementComponent
      },
      {
        path: 'summary',
        component: SummaryComponent
      },
      {
        path: '', // Equivalent to `/` and alias for `profile`
        redirectTo: 'profile',
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolmentRoutingModule { }
