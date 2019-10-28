import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthGuard } from '@auth/shared/guards/auth.guard';

import { EnrolleeGuard } from './shared/guards/enrollee.guard';
import { EnrolmentGuard } from './shared/guards/enrolment.guard';

import { ProfileComponent } from './pages/profile/profile.component';
import { ContactComponent } from './pages/contact/contact.component';
import { ProfessionalInfoComponent } from './pages/professional-info/professional-info.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { PharmanetAccessComponent } from './pages/pharmanet-access/pharmanet-access.component';
import { ReviewComponent } from './pages/review/review.component';
// TODO: temporary until UX is provided
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';



const routes: Routes = [
  {
    path: 'enrolment',
    component: DashboardComponent,
    // Check authentication and authorization each time
    // the router navigates to the next route
    canActivateChild: [
      AuthGuard,
      // Guard module from being accessed without the proper
      // authorization based on the user role permissions
      EnrolleeGuard
    ],
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
        canActivate: [EnrolmentGuard],
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'contact',
        component: ContactComponent,
        canActivate: [EnrolmentGuard],
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'professional',
        component: ProfessionalInfoComponent,
        canActivate: [EnrolmentGuard],
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'declaration',
        component: SelfDeclarationComponent,
        canActivate: [EnrolmentGuard],
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'access',
        component: PharmanetAccessComponent,
        canActivate: [EnrolmentGuard],
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'review',
        canActivate: [EnrolmentGuard],
        component: ReviewComponent
      },
      {
        path: 'confirmation',
        canActivate: [EnrolmentGuard],
        component: ConfirmationComponent
      },
      {
        path: 'agreement',
        canActivate: [EnrolmentGuard],
        component: AccessAgreementComponent
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
