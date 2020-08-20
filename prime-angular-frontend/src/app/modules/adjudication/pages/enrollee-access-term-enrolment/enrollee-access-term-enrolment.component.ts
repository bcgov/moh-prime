import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, forkJoin } from 'rxjs';
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
  public enrolmentProfileHistory: EnrolmentProfileVersion;
  public enrolment: Enrolment;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource,
  ) {
    super(route, router);
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.aid;
    this.busy = forkJoin([
      this.adjudicationResource.getEnrolmentForAccessTerm(enrolleeId, accessTermId),
      this.adjudicationResource.getEnrolleeById(enrolleeId)
    ]).pipe(
      map(([enrolmentProfileVersion, enrolment]: [HttpEnrolleeProfileVersion, HttpEnrollee]) =>
        [this.enrolleeVersionAdapterResponse(enrolmentProfileVersion),
        this.enrolleeAdapterResponse(enrolment)]
      )
    ).subscribe(([enrolmentProfileHistory, enrolment]: [EnrolmentProfileVersion, Enrolment]) => {
      this.enrolmentProfileHistory = enrolmentProfileHistory;
      this.enrolment = enrolment;
    });
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

    if (!enrollee.enrolleeCareSettings) {
      enrollee.enrolleeCareSettings = [];
    }

    return this.enrolmentAdapter(enrollee);
  }

  private enrolmentAdapter(enrollee: HttpEnrollee): Enrolment {
    const {
      userId,
      firstName,
      lastName,
      givenNames,
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
      enrolleeCareSettings,
      ...remainder
    } = enrollee;

    return {
      enrollee: {
        userId,
        firstName,
        lastName,
        givenNames,
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
      careSettings: enrollee.enrolleeCareSettings,
      ...remainder
    };
  }
}
