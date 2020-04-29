import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { SiteRoutes } from './site-registration.routes';
import { RegistrationGuard } from './shared/guards/registration.guard';
import { RegistrantGuard } from './shared/guards/registrant.guard';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { VendorComponent } from './pages/vendor/vendor.component';
import { MultipleSitesComponent } from './pages/multiple-sites/multiple-sites.component';
import { SigningAuthorityComponent } from './pages/signing-authority/signing-authority.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';
import { SiteAddressComponent } from './pages/site-address/site-address.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { TechnicalSupportComponent } from './pages/technical-support/technical-support.component';
import { SiteReviewComponent } from './pages/site-review/site-review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';

const routes: Routes = [
  {
    path: SiteRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [
      AuthenticationGuard,
      RegistrantGuard,
      RegistrationGuard
    ],
    // Ensure that the configuration is loaded, otherwise
    // if it already exists NOOP
    resolve: [ConfigResolver],
    children: [
      {
        path: SiteRoutes.COLLECTION_NOTICE,
        component: CollectionNoticeComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: SiteRoutes.MULTIPLE_SITES,
        component: MultipleSitesComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Multiple Sites' }
      },
      {
        path: SiteRoutes.ORGANIZATION_INFORMATION,
        component: OrganizationInformationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Organization Information' }
      },
      {
        path: SiteRoutes.BUSINESS_LICENCE,
        component: BusinessLicenceComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Business Licence' }
      },
      {
        path: SiteRoutes.SITE_ADDRESS,
        component: SiteAddressComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Site Address' }
      },
      {
        path: SiteRoutes.ORGANIZATION_AGREEMENT,
        component: OrganizationAgreementComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Access Agreement' }
      },
      {
        path: SiteRoutes.VENDOR,
        component: VendorComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Vendor' }
      },
      {
        path: SiteRoutes.HOURS_OPERATION,
        component: HoursOperationComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Hours of Operation' }
      },
      {
        path: SiteRoutes.SIGNING_AUTHORITY,
        component: SigningAuthorityComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Signing Authority' }
      },
      {
        path: SiteRoutes.ADMINISTRATOR,
        component: AdministratorComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Administrator of PharmaNet' }
      },
      {
        path: SiteRoutes.PRIVACY_OFFICER,
        component: PrivacyOfficerComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Privacy Officer' }
      },
      {
        path: SiteRoutes.TECHNICAL_SUPPORT,
        component: TechnicalSupportComponent,
        canDeactivate: [CanDeactivateFormGuard],
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
      },
      {
        path: '', // Equivalent to `/` and alias for `site-review`
        redirectTo: SiteRoutes.SITE_REVIEW,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationRoutingModule { }
