import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { Address } from '@shared/models/address.model';
import { HttpEnrollee, Enrolment } from '@shared/models/enrolment.model';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrolleeProfileVersion, EnrolmentProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-access-term-enrolment',
  templateUrl: './enrollee-access-term-enrolment.component.html',
  styleUrls: ['./enrollee-access-term-enrolment.component.scss']
})
export class EnrolleeAccessTermEnrolmentComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  // TODO replace when Enrolemnt is refactored out of the EnrolmentModule
  // public enrolleeProfileVersion: HttpEnrolleeProfileVersion;
  public enrolmentProfileHistory: EnrolmentProfileVersion;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource,
  ) {
    super(route, router);
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource.getEnrolmentForAccessTerm(enrolleeId, accessTermId, true)
      .pipe(
        map((enrolmentProfileVersion: HttpEnrolleeProfileVersion) => this.enrolleeVersionAdapterResponse(enrolmentProfileVersion))
      )
      // TODO replace when Enrolemnt is refactored out of the EnrolmentModule
      // .subscribe((enrolleeProfileVersion: HttpEnrolleeProfileVersion) =>
      //   this.enrolleeProfileVersion = enrolleeProfileVersion
      // );
      .subscribe((enrolmentProfileVersion: EnrolmentProfileVersion) =>
        this.enrolmentProfileHistory = enrolmentProfileVersion
      );
  }

  private enrolleeVersionAdapterResponse(
    { id, enrolleeId, profileSnapshot, createdDate }: HttpEnrolleeProfileVersion
  ): EnrolmentProfileVersion {
    return {
      id,
      enrolleeId,
      profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
      createdDate
    };
  }

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): Enrolment {
    if (!enrollee.mailingAddress) {
      enrollee.mailingAddress = new Address();
    }

    if (!enrollee.certifications) {
      enrollee.certifications = [];
    }

    if (!enrollee.jobs) {
      enrollee.jobs = [];
    }

    if (!enrollee.enrolleeOrganizationTypes) {
      enrollee.enrolleeOrganizationTypes = [];
    }

    return this.enrolmentAdapter(enrollee);
  }

  private enrolmentAdapter(enrollee: HttpEnrollee): Enrolment {
    const {
      userId,
      firstName,
      middleName,
      lastName,
      preferredFirstName,
      preferredMiddleName,
      preferredLastName,
      dateOfBirth,
      gpid,
      hpdid,
      physicalAddress,
      mailingAddress,
      contactEmail,
      contactPhone,
      voicePhone,
      voiceExtension,
      enrolleeOrganizationTypes,
      ...remainder
    } = enrollee;

    return {
      enrollee: {
        userId,
        firstName,
        middleName,
        lastName,
        preferredFirstName,
        preferredMiddleName,
        preferredLastName,
        dateOfBirth,
        gpid,
        hpdid,
        physicalAddress,
        mailingAddress,
        contactEmail,
        contactPhone,
        voicePhone,
        voiceExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      organizations: enrollee.enrolleeOrganizationTypes,
      ...remainder
    };
  }
}
