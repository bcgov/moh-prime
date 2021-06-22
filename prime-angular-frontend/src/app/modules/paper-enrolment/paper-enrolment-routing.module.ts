import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

import { PaperEnrolmentGuard } from '@paper-enrolment/shared/guards/paper-enrolment.guard';
import { PaperEnrolmentDashboardComponent } from '@paper-enrolment/shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';

import { DemographicPageComponent } from './pages/demographic-page/demographic-page.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { RegulatoryPageComponent } from './pages/regulatory-page/regulatory-page.component';
import { SelfDeclarationPageComponent } from './pages/self-declaration-page/self-declaration-page.component';

const routes: Routes = [
  {
    path: '',
    component: PaperEnrolmentDashboardComponent,
    children: [
      {
        // During initial registration the ID will be set to
        // zero indicating the organization does not exist
        path: ':eid',
        canActivateChild: [PaperEnrolmentGuard],
        children: [
          {
            path: PaperEnrolmentRoutes.DEMOGRAPHIC,
            component: DemographicPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.CARE_SETTING,
            component: CareSettingComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.REGULATORY,
            component: RegulatoryPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.SELF_DECLARATION,
            component: SelfDeclarationPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: '', // Equivalent to `/` and alias for default view
            redirectTo: PaperEnrolmentRoutes.DEMOGRAPHIC,
            pathMatch: 'full'
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PaperEnrolmentRoutingModule {}
