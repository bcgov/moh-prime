import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrollee, Enrolment } from '@shared/models/enrolment.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrolment',
  templateUrl: './enrolment.component.html',
  styleUrls: ['./enrolment.component.scss']
})
export class EnrolmentComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public enrollee: Enrolment;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(route, router);
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit() {
    this.getEnrollee(this.route.snapshot.params.id);
  }

  private getEnrollee(enrolleeId: number) {
    this.busy = this.adjudicationResource.getEnrolleeById(enrolleeId)
      .pipe(
        map((enrollee: HttpEnrollee) => this.enrolleeAdapterResponse(enrollee))
      )
      .subscribe((enrollee: Enrolment) => this.enrollee = enrollee);
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
      id,
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
      ...remainder
    } = enrollee;

    return {
      id,
      enrollee: {
        id,
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
