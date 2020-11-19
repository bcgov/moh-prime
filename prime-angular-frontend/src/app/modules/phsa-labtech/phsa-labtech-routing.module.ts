import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PhsaLabtechRoutes } from './phsa-labtech.routes';
import { AccessCodeComponent } from './pages/access-code/access-code.component';

import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: PhsaLabtechDashboardComponent,
    children: [
      {
        path: PhsaLabtechRoutes.ACCESS_CODE,
        component: AccessCodeComponent,
        data: { title: 'Access Code' }
      },
      {
        path: '', // Equivalent to `/` and alias for `access-code`
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
