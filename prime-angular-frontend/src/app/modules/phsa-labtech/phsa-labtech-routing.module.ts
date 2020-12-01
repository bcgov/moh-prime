import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConfigGuard } from '@config/config.guard';

import { PhsaLabtechRoutes } from './phsa-labtech.routes';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { ExampleComponent } from './pages/example/example.component';

const routes: Routes = [
  {
    path: PhsaLabtechRoutes.MODULE_PATH,
    component: PhsaLabtechDashboardComponent,
    canActivate: [
      // Ensure that the configuration is loaded prior to dependent
      // guards, as well as, views, otherwise if it already exists NOOP
      // NOTE: A resolver could not be used due to their execution
      // occuring after parent and child guards
      ConfigGuard
    ],
    children: [
      {
        path: PhsaLabtechRoutes.EXAMPLE,
        component: ExampleComponent,
        data: { title: 'Example' }
      },
      {
        path: PhsaLabtechRoutes.BCSC_DEMOGRAPHIC,
        component: BcscDemographicComponent,
        data: { title: 'PRIME Enrolment' }
      },
      {
        path: '', // Equivalent to `/` and alias for the default route
        redirectTo: PhsaLabtechRoutes.BCSC_DEMOGRAPHIC,
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
