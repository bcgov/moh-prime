import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { PaperEnrolmentRoutes } from './paper-enrolment.routes';
import { PaperEnrolmentGuard } from './shared/guards/paper-enrolment.guard';
import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';

import { DemographicPageComponent } from './pages/demographic-page/demographic-page.component';
import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { RegulatoryPageComponent } from './pages/regulatory-page/regulatory-page.component';
import { SelfDeclarationPageComponent } from './pages/self-declaration-page/self-declaration-page.component';
import { OboSitesPageComponent } from './pages/obo-sites-page/obo-sites-page.component';
import { OverviewPageComponent } from './pages/overview-page/overview-page.component';
import { UploadPageComponent } from './pages/upload-page/upload-page.component';
import { NextStepsPageComponent } from './pages/next-steps-page/next-steps-page.component';
import { FinalizedEnrolmentGuard } from './shared/guards/finalized-enrolment.guard';

const routes: Routes = [
  {
    path: '',
    component: PaperEnrolmentDashboardComponent,
    children: [
      {
        // During initial registration the ID will be set to
        // zero indicating the organization does not exist
        path: ':eid',
        // canActivateChild: [PaperEnrolmentGuard],
        canActivate: [FinalizedEnrolmentGuard],
        children: [
          {
            path: PaperEnrolmentRoutes.DEMOGRAPHIC,
            component: DemographicPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.CARE_SETTING,
            component: CareSettingPageComponent,
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
            path: PaperEnrolmentRoutes.OBO_SITES,
            component: OboSitesPageComponent,
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
            path: PaperEnrolmentRoutes.UPLOAD,
            component: UploadPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.OVERVIEW,
            component: OverviewPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'PRIME Profile' }
          },
          {
            path: PaperEnrolmentRoutes.NEXT_STEPS,
            component: NextStepsPageComponent,
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
export class PaperEnrolmentRoutingModule { }
