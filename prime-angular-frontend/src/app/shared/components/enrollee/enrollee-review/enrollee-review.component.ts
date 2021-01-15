import { Component, Input, Output, EventEmitter } from '@angular/core';

import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { RemoteAccessSite } from '@enrolment/shared/models/remote-access-site.model';
import { RemoteAccessLocation } from '@enrolment/shared/models/remote-access-location.model';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

@Component({
  selector: 'app-enrollee-review',
  templateUrl: './enrollee-review.component.html',
  styleUrls: ['./enrollee-review.component.scss']
})
export class EnrolleeReviewComponent {
  @Input() public showEditRedirect: boolean;
  @Input() public enrolment: Enrolment;
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
    private enrolmentService: EnrolmentService
  ) {
    this.showEditRedirect = false;
    this.admin = false;
    this.route = new EventEmitter<string | (string | number)[]>();

    this.authService.identityProvider$()
      .subscribe((identityProvider: IdentityProviderEnum) => {
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

  public get hasMailingAddress(): boolean {
    return (this.enrollee && this.enrollee.mailingAddress && !!this.enrollee.mailingAddress.countryCode);
  }

  public get hasCertification(): boolean {
    return (this.enrolment && !!this.enrolment.certifications.length);
  }

  public get certifications(): CollegeCertification[] {
    return (this.hasCertification) ? this.enrolment.certifications : [];
  }

  public get hasDeviceProviderNumber(): boolean {
    return (this.enrolment && !!this.enrolment.deviceProviderNumber);
  }

  public get hasJob(): boolean {
    return (this.enrolment && !!this.enrolment.jobs.length);
  }

  public get jobs(): Job[] {
    return (this.hasJob) ? this.enrolment.jobs : [];
  }

  public get oboSites(): OboSite[] {
    return this.enrolment.oboSites ? this.enrolment.oboSites : [];
  }

  public get hasCareSetting(): boolean {
    return (this.enrolment && !!this.enrolment.careSettings.length);
  }

  public get careSettings(): CareSetting[] {
    return (this.hasCareSetting) ? this.enrolment.careSettings : [];
  }

  public get healthAuthorities(): { healthAuthorityCode: number, facilityCodes: number[] }[] {
    const healthAuthoritiesGrouped = this.enrolment?.enrolleeHealthAuthorities
      ?.reduce((grouped: { [key: number]: number[] }, ha: HealthAuthority) => {
        grouped[ha.healthAuthorityCode] = [].concat([...(grouped[ha.healthAuthorityCode] ?? []), ha.facilityCode]);
        return grouped;
      }, {});

    const healthAuthorities = healthAuthoritiesGrouped
      ? Object.keys(healthAuthoritiesGrouped)
        .map(key => ({
          healthAuthorityCode: +key,
          facilityCodes: healthAuthoritiesGrouped[key]
        }))
      : null;

    return (healthAuthorities?.length) ? healthAuthorities : [];
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

  public onRoute(routePath: string | (string | number)[], event?: Event): void {
    event?.preventDefault();
    this.route.emit(routePath);
  }
}
