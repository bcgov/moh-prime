import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PhsaLabtechRoutes } from './phsa-labtech.routes';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { AccessCodeComponent } from './pages/access-code/access-code.component';

const routes: Routes = [
  {
    path: '',
    component: PhsaLabtechDashboardComponent,
    children: [
      {
        path: PhsaLabtechRoutes.ACCESS_CODE,
        component: AccessCodeComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Access Code' }
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
