import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnderagedGuard } from '@core/guards/underaged.guard';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { SiteRoutes } from './site-registration.routes';
import { RegistrantGuard } from './shared/guards/registrant.guard';
import { SiteRegistrationDashboardComponent } from './shared/components/site-registration-dashboard/site-registration-dashboard.component';

import { changeSigningAuthorityWorkflow, defaultCommunitySiteWorkflow } from '@registration/site-registration-routing.workflows';

const routes: Routes = [
  {
    path: '',
    component: SiteRegistrationDashboardComponent,
    canActivate: [
      AuthenticationGuard,
      UnderagedGuard
    ],
    canActivateChild: [
      AuthenticationGuard,
      RegistrantGuard
    ],
    children: [
      {
        path: SiteRoutes.COMMUNITY_SITE_DEFAULT_WORKFLOW,
        canActivateChild: [],
        children: defaultCommunitySiteWorkflow
      },
      {
        path: SiteRoutes.CHANGE_SIGNING_AUTHORITY_WORKFLOW,
        canActivateChild: [],
        children: changeSigningAuthorityWorkflow
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationRoutingModule {}
