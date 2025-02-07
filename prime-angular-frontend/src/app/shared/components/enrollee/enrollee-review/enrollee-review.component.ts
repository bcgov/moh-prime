import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ValidationErrors } from '@angular/forms';

import { RoutePath } from '@lib/utils/route-utils.class';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';
import { EnrolleeDeviceProvider } from '@shared/models/enrollee-device-provider.model';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-enrollee-review',
  templateUrl: './enrollee-review.component.html',
  styleUrls: ['./enrollee-review.component.scss']
})
export class EnrolleeReviewComponent {
  @Input() public showEditRedirect: boolean;
  @Input() public enrolment: Enrolment;
  @Input() public enrolmentErrors: ValidationErrors;
  @Input() public admin: boolean;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public demographicRoutePath: string;
  public identityProvider: IdentityProviderEnum;
  public IdentityProviderEnum = IdentityProviderEnum;
  public CareSettingEnum = CareSettingEnum;
  public EnrolmentRoutes = EnrolmentRoutes;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService,
    private enrolmentService: EnrolmentService,
    private configService: ConfigService,
  ) {
    this.showEditRedirect = false;
    this.admin = false;
    this.route = new EventEmitter<string | (string | number)[]>();

    this.authService.identityProvider$()
      .subscribe((identityProvider: IdentityProviderEnum) => {
        // Note that identityProvider may be equal to IdentityProviderEnum.IDIR
        this.identityProvider = identityProvider;
        this.demographicRoutePath = (identityProvider === IdentityProviderEnum.BCEID)
          ? EnrolmentRoutes.BCEID_DEMOGRAPHIC
          : EnrolmentRoutes.BCSC_DEMOGRAPHIC;
      });
  }

  public get enrollee() {
    return (this.enrolment) ? this.enrolment.enrollee : null;
  }

  public get hasPreferredName(): boolean {
    return (
      this.enrollee &&
      (
        !!this.enrollee.preferredFirstName ||
        !!this.enrollee.preferredMiddleName ||
        !!this.enrollee.preferredLastName
      )
    );
  }

  public get hasCertification(): boolean {
    return (this.enrolment && !!this.enrolment.certifications.length);
  }

  public get certifications(): CollegeCertification[] {
    return (this.hasCertification)
      ? this.enrolment.certifications
      : [];
  }

  public get hasDeviceProvider(): boolean {
    return (this.enrolment && !!this.enrolment.enrolleeDeviceProviders?.length);
  }

  public get enrolleeDeviceProvider(): EnrolleeDeviceProvider {
    return (this.hasDeviceProvider)
      ? this.enrolment.enrolleeDeviceProviders[0]
      : null;
  }

  public get hasUnlistedCertification(): boolean {
    return (this.enrolment && !!this.enrolment.unlistedCertifications.length);
  }

  public get unlistedCertifications(): UnlistedCertification[] {
    return (this.hasUnlistedCertification)
      ? this.enrolment.unlistedCertifications
      : [];
  }

  public get hasCareSetting(): boolean {
    return (this.enrolment && !!this.enrolment.careSettings.length);
  }

  public get healthAuthorities(): { healthAuthorityCode: number }[] {
    const healthAuthoritiesGrouped = this.enrolment?.enrolleeHealthAuthorities
      ?.reduce((grouped: { [key: number]: number[] }, ha: EnrolleeHealthAuthority) => {
        grouped[ha.healthAuthorityCode] = [].concat([...(grouped[ha.healthAuthorityCode] ?? [])]);
        return grouped;
      }, {});

    const healthAuthorities = healthAuthoritiesGrouped
      ? Object.keys(healthAuthoritiesGrouped)
        .map(key => ({ healthAuthorityCode: +key }))
      : null;

    return (healthAuthorities?.length) ? healthAuthorities : [];
  }

  public getLicenceClassCategory(certification: CollegeCertification): string {
    let grouping = this.configService.licenses.find(l => l.code === certification.licenseCode).collegeLicenses.map(cl => cl.collegeLicenseGroupingCode);

    if (grouping && grouping.length > 0 && grouping[0]) {
      return `${this.configService.collegeLicenseGroupings.find(g => g.code === grouping[0]).name} - `;
    } else {
      return '';
    }
  }

  public get isRequestingRemoteAccess(): boolean {
    return (this.enrolment && !!this.enrolment.enrolleeRemoteUsers?.length);
  }

  public get remoteAccessSites(): RemoteAccessSite[] {
    return (this.isRequestingRemoteAccess)
      ? this.enrolment.remoteAccessSites
      : [];
  }

  public get remoteAccessLocations(): RemoteAccessLocation[] {
    return (this.isRequestingRemoteAccess)
      ? this.enrolment.remoteAccessLocations
      : [];
  }

  public showCollegePrefix(licenceCode: number, collegeCode: number): number {
    return (this.enrolmentService.shouldShowCollegePrefix(licenceCode))
      ? collegeCode
      : null;
  }

  public getRemoteAccessSiteVendor(remoteAccessSiteId: number) {
    const ras = this.remoteAccessSites.find((site => site.id === remoteAccessSiteId));
    return ras.site.siteVendors?.length ? ras.site.siteVendors[0].vendorCode : null;
  }

  public onRoute(routePath: RoutePath, event?: Event): void {
    event?.preventDefault();
    this.route.emit(routePath);
  }

  public isPaperEnrollee(enrollee): boolean {
    return this.enrolmentService.isPaperEnrollee(enrollee);
  }

}
