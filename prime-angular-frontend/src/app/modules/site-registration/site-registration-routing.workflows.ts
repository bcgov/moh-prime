import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationGuard } from '@registration/shared/guards/organization.guard';
import { SiteGuard } from '@registration/shared/guards/site.guard';
import { ElectronicAgreementGuard } from '@registration/shared/guards/electronic-agreement.guard';
import { PendingTransferGuard } from '@registration/shared/guards/pending-transfer.guard';
import { ChangeSigningAuthorityGuard } from '@registration/shared/guards/change-signing-authority.guard';

import { CollectionNoticePageComponent } from '@registration/pages/collection-notice-page/collection-notice-page.component';
import { OrganizationSigningAuthorityPageComponent } from '@registration/pages/organization-signing-authority-page/organization-signing-authority-page.component';
import { OrganizationClaimPageComponent } from '@registration/pages/organization-claim-page/organization-claim-page.component';
import { OrganizationClaimConfirmationPageComponent } from '@registration/pages/organization-claim-confirmation-page/organization-claim-confirmation-page.component';
import { OrganizationNamePageComponent } from '@registration/pages/organization-name-page/organization-name-page.component';
import { SiteManagementPageComponent } from '@registration/pages/site-management-page/site-management-page.component';
import { CareSettingPageComponent } from '@registration/pages/care-setting-page/care-setting-page.component';
import { BusinessLicencePageComponent } from '@registration/pages/business-licence-page/business-licence-page.component';
import { BusinessLicenceRenewalPageComponent } from '@registration/pages/business-licence-renewal-page/business-licence-renewal-page.component';
import { SiteAddressPageComponent } from '@registration/pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from '@registration/pages/hours-operation-page/hours-operation-page.component';
import { DeviceProviderPageComponent } from '@registration/pages/device-provider-page/device-provider-page.component';
import { RemoteUsersPageComponent } from '@registration/pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from '@registration/pages/remote-user-page/remote-user-page.component';
import { AdministratorPageComponent } from '@registration/pages/administrator-page/administrator-page.component';
import { PrivacyOfficerPageComponent } from '@registration/pages/privacy-officer-page/privacy-officer-page.component';
import { TechnicalSupportPageComponent } from '@registration/pages/technical-support-page/technical-support-page.component';
import { OrganizationAgreementPageComponent } from '@registration/pages/organization-agreement-page/organization-agreement-page.component';
import { ElectronicOrganizationAgreementPageComponent } from '@registration/pages/electronic-organization-agreement-page/electronic-organization-agreement-page.component';
import { OverviewPageComponent } from '@registration/pages/overview-page/overview-page.component';
import { NextStepsPageComponent } from '@registration/pages/next-steps-page/next-steps-page.component';

/**
 * @description
 * Routing workflow used as the default workflow for
 * create an organization and their associated sites.
 */
export const defaultCommunitySiteWorkflow = [
  {
    path: SiteRoutes.COLLECTION_NOTICE,
    component: CollectionNoticePageComponent,
    data: {
      title: 'Collection Notice',
      // Provides the next route segments so route
      // configuration drives routing vs the component
      redirectRouteSegments: {
        nextRoute: SiteRoutes.ORGANIZATIONS
      }
    }
  },
  {
    path: SiteRoutes.ORGANIZATIONS,
    children: [
      {
        path: '',
        component: SiteManagementPageComponent,
        canActivate: [OrganizationGuard],
        data: { title: 'Site Management' }
      },
      {
        // During initial registration the ID will be set to
        // zero indicating the organization does not exist
        path: ':oid',
        canActivateChild: [OrganizationGuard],
        children: [
          {
            path: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY,
            component: OrganizationSigningAuthorityPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Signing Authority' }
          },
          {
            path: SiteRoutes.ORGANIZATION_NAME,
            component: OrganizationNamePageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Organization Information' }
          },
          {
            path: SiteRoutes.ORGANIZATION_REVIEW,
            component: OverviewPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Organization Review' }
          },
          {
            path: '', // Equivalent to `/` and alias for `organization-review`
            redirectTo: SiteRoutes.ORGANIZATION_REVIEW,
            pathMatch: 'full'
          },
          {
            path: `${SiteRoutes.CARE_SETTINGS}/:csid/${SiteRoutes.ORGANIZATION_AGREEMENT}`,
            component: ElectronicOrganizationAgreementPageComponent,
            canActivate: [ElectronicAgreementGuard],
            data: { title: 'Organization Agreement' }
          },
          {
            path: `${SiteRoutes.SITES}/:sid`,
            canActivateChild: [PendingTransferGuard],
            children: [
              {
                path: SiteRoutes.CARE_SETTING,
                component: CareSettingPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Care Setting' }
              },
              {
                path: SiteRoutes.BUSINESS_LICENCE,
                component: BusinessLicencePageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Site Business Licence' }
              },
              {
                path: SiteRoutes.BUSINESS_LICENCE_RENEWAL,
                component: BusinessLicenceRenewalPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Site Business Licence' }
              },
              {
                path: SiteRoutes.SITE_ADDRESS,
                component: SiteAddressPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Site Address' }
              },
              {
                path: SiteRoutes.HOURS_OPERATION,
                component: HoursOperationPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Hours of Operation' }
              },
              {
                path: SiteRoutes.DEVICE_PROVIDER,
                component: DeviceProviderPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Individual Device Providers' }
              },
              {
                path: SiteRoutes.REMOTE_USERS,
                children: [
                  {
                    path: '',
                    component: RemoteUsersPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Practitioners Requiring Remote PharmaNet Access' },
                  },
                  {
                    path: ':index',
                    component: RemoteUserPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Remote User' }
                  }
                ]
              },
              {
                path: SiteRoutes.ADMINISTRATOR,
                component: AdministratorPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'PharmaNet Administrator' }
              },
              {
                path: SiteRoutes.PRIVACY_OFFICER,
                component: PrivacyOfficerPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Privacy Officer' }
              },
              {
                path: SiteRoutes.TECHNICAL_SUPPORT,
                component: TechnicalSupportPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Technical Support Contact' }
              },
              {
                path: SiteRoutes.ORGANIZATION_AGREEMENT,
                component: OrganizationAgreementPageComponent,
                canActivate: [SiteGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Agreement' }
              },
              {
                path: SiteRoutes.SITE_REVIEW,
                component: OverviewPageComponent,
                canActivate: [SiteGuard],
                data: { title: 'Site Registration Review' }
              },
              {
                path: SiteRoutes.NEXT_STEPS,
                canActivate: [SiteGuard],
                component: NextStepsPageComponent,
                data: { title: 'Next Steps' }
              },
              {
                path: '', // Equivalent to `/` and alias for default route
                redirectTo: SiteRoutes.SITE_REVIEW,
                pathMatch: 'full'
              }
            ]
          }
        ]
      }
    ]
  },
  {
    path: '', // Equivalent to `/` and alias for default route
    redirectTo: SiteRoutes.ORGANIZATIONS,
    pathMatch: 'full'
  }
];

/**
 * @description
 * Routing workflow for changing an organization's
 * signing authority.
 */
export const changeSigningAuthorityWorkflow = [
  {
    path: SiteRoutes.COLLECTION_NOTICE,
    component: CollectionNoticePageComponent,
    data: {
      title: 'Collection Notice',
      // Provides a workflow and next route segments so route
      // configuration drives routing vs the component
      redirectRouteSegments: {
        nextRoute: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY
      }
    }
  },
  {
    path: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY,
    component: OrganizationSigningAuthorityPageComponent,
    canActivate: [ChangeSigningAuthorityGuard],
    canDeactivate: [CanDeactivateFormGuard],
    data: {
      title: 'Signing Authority',
      // Provides the next route segments so route
      // configuration drives routing vs the component
      redirectRouteSegments: {
        nextRoute: SiteRoutes.ORGANIZATION_CLAIM
      }
    }
  },
  {
    path: SiteRoutes.ORGANIZATION_CLAIM,
    component: OrganizationClaimPageComponent,
    canActivate: [ChangeSigningAuthorityGuard],
    canDeactivate: [CanDeactivateFormGuard],
    data: { title: 'Claim Organization' }
  },
  {
    path: SiteRoutes.ORGANIZATION_CLAIM_CONFIRMATION,
    component: OrganizationClaimConfirmationPageComponent,
    canActivate: [ChangeSigningAuthorityGuard],
    canDeactivate: [CanDeactivateFormGuard],
    data: { title: 'Next Steps' }
  },
  {
    path: '', // Equivalent to `/` and alias for default route
    redirectTo: SiteRoutes.COLLECTION_NOTICE,
    pathMatch: 'full'
  }
];
