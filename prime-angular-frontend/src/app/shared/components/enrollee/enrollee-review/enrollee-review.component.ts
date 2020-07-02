import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { OrganizationType } from '@enrolment/shared/models/organization-type.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

@Component({
  selector: 'app-enrollee-review',
  templateUrl: './enrollee-review.component.html',
  styleUrls: ['./enrollee-review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleeReviewComponent {
  @Input() public showEditRedirect: boolean;
  @Input() public enrolment: Enrolment;
  @Output() public route: EventEmitter<string>;

  public EnrolmentRoutes = EnrolmentRoutes;

  constructor() {
    this.showEditRedirect = false;
    this.route = new EventEmitter<string>();
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

  public get hasOrganization(): boolean {
    return (this.enrolment && !!this.enrolment.organizations.length);
  }

  public get isRequestingRemoteAccess(): boolean {
    return (this.enrolment && !!this.enrolment.requestingRemoteAccess);
  }

  public get organizations(): OrganizationType[] {
    return (this.hasOrganization) ? this.enrolment.organizations : [];
  }

  public onRoute(routePath: string): void {
    this.route.emit(routePath);
  }

  public hasConviction(): boolean {
    return this.hasSelfDeclaration(SelfDeclarationTypeEnum.HAS_CONVICTION);
  }

  public hasRegistrationSuspended(): boolean {
    return this.hasSelfDeclaration(SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED);
  }

  public hasDisciplinaryAction(): boolean {
    return this.hasSelfDeclaration(SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION);
  }

  public hasPharmaNetSuspended(): boolean {
    return this.hasSelfDeclaration(SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED);
  }

  public getConvictionDetails(): string {
    return this.getSelfDeclarationDetailsIfExist(SelfDeclarationTypeEnum.HAS_CONVICTION);
  }

  public getRegistrationSuspendedDetails(): string {
    return this.getSelfDeclarationDetailsIfExist(SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED);
  }

  public getDisciplinaryActionDetails(): string {
    return this.getSelfDeclarationDetailsIfExist(SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION);
  }

  public getPharmaNetSuspendedDetails(): string {
    return this.getSelfDeclarationDetailsIfExist(SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED);
  }

  private hasSelfDeclaration(type: SelfDeclarationTypeEnum): boolean {
    return !!this.enrolment?.selfDeclarations
      .filter((decl) => decl.selfDeclarationTypeCode === type).length;
  }

  private getSelfDeclarationDetailsIfExist(type: SelfDeclarationTypeEnum): string {
    const declaration = this.enrolment?.selfDeclarations
      .filter((decl) => decl.selfDeclarationTypeCode === type).pop();
    return declaration?.selfDeclarationDetails ?? '';
  }

}
