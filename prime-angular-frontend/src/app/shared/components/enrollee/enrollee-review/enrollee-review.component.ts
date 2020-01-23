import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';

import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { Job } from '@enrolment/shared/models/job.model';
import { Organization } from '@enrolment/shared/models/organization.model';

@Component({
  selector: 'app-enrollee-review',
  templateUrl: './enrollee-review.component.html',
  styleUrls: ['./enrollee-review.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleeReviewComponent {
  @Input() public isEnrollee: boolean;
  @Input() public enrolment: Enrolment;
  @Output() public route: EventEmitter<string>;

  public EnrolmentRoutes = EnrolmentRoutes;

  constructor() {
    this.isEnrollee = true;
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

  public get organizations(): Organization[] {
    return (this.hasOrganization) ? this.enrolment.organizations : [];
  }

  public onRoute(routePath: string): void {
    this.route.emit(routePath);
  }
}
