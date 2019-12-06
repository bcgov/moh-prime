import { Component, OnInit, Input } from '@angular/core';

import { Enrolment } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-enrollee-review',
  templateUrl: './enrollee-review.component.html',
  styleUrls: ['./enrollee-review.component.scss']
})
export class EnrolleeReviewComponent implements OnInit {
  @Input() public isEnrolling: boolean;
  @Input() public enrolment: Enrolment;

  constructor() {
    this.isEnrolling = true;
  }

  public get enrollee() {
    return (this.enrolment) ? this.enrolment.enrollee : null;
  }

  public get physicalAddress() {
    return (this.enrollee) ? this.enrollee.physicalAddress : null;
  }

  public get mailingAddress() {
    return (this.enrollee) ? this.enrollee.mailingAddress : null;
  }

  public get hasCertifications(): boolean {
    return (this.enrolment && !!this.enrolment.certifications.length);
  }

  public get certifications() {
    return (this.hasCertifications) ? this.enrolment.certifications : [];
  }

  public get hasJobs(): boolean {
    return (this.enrolment && !!this.enrolment.jobs.length);
  }

  public get jobs() {
    return (this.hasJobs) ? this.enrolment.jobs : [];
  }

  public get hasOrganizations(): boolean {
    return (this.enrolment && !!this.enrolment.organizations.length);
  }

  public get organizations() {
    return (this.hasOrganizations) ? this.enrolment.organizations : [];
  }

  public showYesNo(declared: boolean) {
    return (declared === null) ? 'N/A'
      : (declared) ? 'Yes' : 'No';
  }

  public ngOnInit() { }
}
