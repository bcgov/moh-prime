import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';

import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { HttpEnrollee, Enrolment } from '@shared/models/enrolment.model';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrolleeSubmission, EnrolmentSubmission } from '@shared/models/enrollee-submission.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-access-term-enrolment',
  templateUrl: './enrollee-access-term-enrolment.component.html',
  styleUrls: ['./enrollee-access-term-enrolment.component.scss']
})
export class EnrolleeAccessTermEnrolmentComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public enrolmentSubmission: EnrolmentSubmission;
  public enrolment: Enrolment;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource,
  ) {
    super(route, router);
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.aid;
    this.busy = forkJoin([
      this.adjudicationResource.getSubmissionForAgreement(enrolleeId, accessTermId),
      this.adjudicationResource.getEnrolleeById(enrolleeId)
    ]).pipe(
      map(([enrolleeSubmission, enrolment]: [HttpEnrolleeSubmission, HttpEnrollee]) =>
        [
            this.enrolleeSubmissionAdapterResponse(enrolleeSubmission),
            this.enrolleeAdapterResponse(enrolment)
        ]
      )
    ).subscribe(([enrolmentSubmission, enrolment]: [EnrolmentSubmission, Enrolment]) => {
      this.enrolmentSubmission = enrolmentSubmission;
      this.enrolment = enrolment;
    });
  }

  private enrolleeSubmissionAdapterResponse(
    { id, enrolleeId, profileSnapshot, agreementType, createdDate }: HttpEnrolleeSubmission
  ): EnrolmentSubmission {
    return {
      id,
      enrolleeId,
      profileSnapshot: this.enrolleeAdapterResponse(profileSnapshot),
      agreementType,
      createdDate
    };
  }

  private enrolleeAdapterResponse(enrollee: HttpEnrollee): Enrolment {
    addressTypes.forEach((addressType: AddressType) => {
      if (!enrollee[addressType]) {
        enrollee[addressType] = new Address();
      }
    });

    if (!enrollee.certifications) {
      enrollee.certifications = [];
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
      verifiedAddress,
      mailingAddress,
      physicalAddress,
      email,
      smsPhone,
      phone,
      phoneExtension,
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
        verifiedAddress,
        mailingAddress,
        physicalAddress,
        email,
        smsPhone,
        phone,
        phoneExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      careSettings: enrollee.enrolleeCareSettings,
      ...remainder
    };
  }
}
