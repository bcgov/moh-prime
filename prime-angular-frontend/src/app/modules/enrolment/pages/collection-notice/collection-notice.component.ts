import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { exhaustMap, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { Enrollee } from '@shared/models/enrollee.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';


@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  public isFull: boolean;
  public bcscUser: BcscUser;

  private potentialPaperEnrolleeReturnee;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource
  ) {
    this.isFull = true;
    this.potentialPaperEnrolleeReturnee = false;
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;
    const nextRoute = this.potentialPaperEnrolleeReturnee
      ? EnrolmentRoutes.PAPER_ENROLLEE_RETURNEE_DECLARATION
      : EnrolmentRoutes.BCSC_DEMOGRAPHIC;


    const route = (!this.enrolmentService.isProfileComplete)
      ? nextRoute
      : EnrolmentRoutes.OVERVIEW;

    this.router.navigate([route], { relativeTo: this.route.parent });
  }

  public ngOnInit(): void {
    this.authService.hasJustLoggedIn = true;
    this.isPotentialPaperEnrolleeReturnee();

    // Collection notice is the initial route after login, and used as a hub
    // for redirection to an appropriate view based on the enrolment
    switch (this.enrolmentService.enrolment?.currentStatus.statusCode) {
      case EnrolmentStatusEnum.UNDER_REVIEW:
        this.router.navigate([EnrolmentRoutes.SUBMISSION_CONFIRMATION], { relativeTo: this.route.parent });
        break;
      case EnrolmentStatusEnum.REQUIRES_TOA:
        this.router.navigate([EnrolmentRoutes.PENDING_ACCESS_TERM], { relativeTo: this.route.parent });
        break;
    }
  }

  private isPotentialPaperEnrolleeReturnee(): void {
    this.getUser$()
      .subscribe(enrollee => {
        this.enrolmentResource.getPotentialPaperEnrolleeReturneeStatus(enrollee.dateOfBirth)
          .subscribe((result: boolean) => this.potentialPaperEnrolleeReturnee = result);
      })
  }

  private getUser$(): Observable<Enrollee> {
    return this.authService.getUser$()
      .pipe(
        map(({ dateOfBirth }: BcscUser) => {
          // Enforced the enrollee type instead of using Partial<Enrollee>
          // to avoid creating constructors and partials for every model
          return {
            // Providing only the minimum required fields for creating an enrollee
            dateOfBirth,
          } as Enrollee;
        })
      );
  }
}
