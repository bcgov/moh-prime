import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { ProfileComponent } from './pages/profile/profile.component';
import { ContactComponent } from './pages/contact/contact.component';
import { ProfessionalInfoComponent } from './pages/professional-info/professional-info.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { PharmanetAccessComponent } from './pages/pharmanet-access/pharmanet-access.component';
import { ReviewComponent } from './pages/review/review.component';
// TODO: temporary until UX is provided
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';

import { DashboardComponent } from './shared/components/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: 'enrolment',
    component: DashboardComponent,
    // TODO: apply guards for loading
    // canLoad: [],
    // canActivate: [],
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
        canDeactivate: [CanDeactivateFormGuard]
      },
      {
        path: 'contact',
        component: ContactComponent,
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
