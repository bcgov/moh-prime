import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PhsaLabtechRoutes } from './phsa-labtech.routes';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';
import { PhsaLabtechGuard } from './shared/guards/phsa-labtech.guard';

const routes: Routes = [
  {
    path: PhsaLabtechRoutes.MODULE_PATH,
    component: PhsaLabtechDashboardComponent,
    canActivate: [
      AuthenticationGuard
    ],
    canActivateChild: [
      PhsaLabtechGuard
    ],
    children: [
      {
        path: PhsaLabtechRoutes.ACCESS_CODE,
        component: AccessCodeComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Access Code' }
      },
      {
        path: PhsaLabtechRoutes.DEMOGRAPHIC,
        component: BcscDemographicComponent,
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: '', // Equivalent to `/` and alias for the default route
        redirectTo: PhsaLabtechRoutes.ACCESS_CODE,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PhsaLabtechRoutingModule { }
