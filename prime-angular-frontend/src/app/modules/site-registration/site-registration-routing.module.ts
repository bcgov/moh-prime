import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';

import { SiteRoutes } from './site-registration.routes';
import { SiteCollectionNoticeComponent } from './pages/site-collection-notice/site-collection-notice.component';
import { VendorComponent } from './pages/vendor/vendor.component';
import { SigningAuthorityComponent } from './pages/signing-authority/signing-authority.component';
import { MultipleSitesComponent } from './pages/multiple-sites/multiple-sites.component';
import { SiteInformationComponent } from './pages/site-information/site-information.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { TechnicalSupportContactComponent } from './pages/technical-support-contact/technical-support-contact.component';
import { SiteReviewComponent } from './pages/site-review/site-review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

const routes: Routes = [
  {
    path: SiteRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [
      // AuthenticationGuard,
      // SiteRegistrationGuard
    ],
    // Ensure that the configuration is loaded, otherwise
    // if it already exists NOOP
    resolve: [ConfigResolver],
    children: [
      {
        path: SiteRoutes.COLLECTION_NOTICE,
        component: SiteCollectionNoticeComponent,
        data: { title: 'Site Collection Notice' }
      },
      {
        path: SiteRoutes.MULTIPLE_SITES,
        component: MultipleSitesComponent,
        data: { title: 'Multiple Sites' }
      },
      {
        path: SiteRoutes.SITE_INFORMATION,
        component: SiteInformationComponent,
        data: { title: 'Site Information' }
      },
      {
        path: SiteRoutes.HOURS_OPERATION,
        component: HoursOperationComponent,
        data: { title: 'Hours of Operation' }
      },
      {
        path: SiteRoutes.VENDOR,
        component: VendorComponent,
        data: { title: 'Vendor' }
      },
      {
        path: SiteRoutes.SIGNING_AUTHORITY,
        component: SigningAuthorityComponent,
        data: { title: 'Signing Authority' }
      },
      {
        path: SiteRoutes.ADMINISTRATOR,
        component: AdministratorComponent,
        data: { title: 'Administrator of PharmaNet' }
      },
      {
        path: SiteRoutes.PRIVACY_OFFICER,
        component: PrivacyOfficerComponent,
        data: { title: 'Privacy Officer' }
      },
      {
        path: SiteRoutes.TECHNICAL_SUPPORT_CONTACT,
        component: TechnicalSupportContactComponent,
        data: { title: 'Technical Support Contact' }
      },
      {
        path: SiteRoutes.SITE_REVIEW,
        component: SiteReviewComponent,
        data: { title: 'Site Registration Review' }
      },
      {
        path: SiteRoutes.CONFIRMATION,
        component: ConfirmationComponent,
        data: { title: 'Submission Confirmation' }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationRoutingModule { }
