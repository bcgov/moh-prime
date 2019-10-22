import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ApplicationsComponent } from './pages/applications/applications.component';
import { ApplicationComponent } from './pages/application/application.component';

const routes: Routes = [
  // path: 'provision',
  // component: DashboardComponent,
  // // TODO: apply guards for loading
  // // canLoad: [],
  // // canActivate: [],
  // children: [
  //   {
  //     path: 'applications',
  //     component: ApplicationsComponent,
  //     canDeactivate: []
  //   },
  //   {
  //     path: 'application',
  //     component: ApplicationComponent,
  //     canDeactivate: []
  //   },
  //   {
  //     path: '', // Equivalent to `/` and alias for `applications`
  //     redirectTo: 'applications',
  //     pathMatch: 'full'
  //   }
  // ]
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProvisionRoutingModule { }
