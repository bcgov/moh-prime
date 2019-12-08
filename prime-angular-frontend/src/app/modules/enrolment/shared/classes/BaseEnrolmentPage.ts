import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

export interface IBaseEnrolmentPage {
  busy: Subscription;
  isProfileComplete: boolean;
  hasInitialStatus: boolean;
  EnrolmentRoutes: EnrolmentRoutes;
  routeTo(routePath: EnrolmentRoutes): void;
}

export abstract class BaseEnrolmentPage implements IBaseEnrolmentPage {
  public busy: Subscription;

  // Whether the enrollee has completed filling in the entire enrolment, and
  // has made it to enrolment review after completion
  public isProfileComplete: boolean;

  // Whether the initial enrolment has been submitted for adjudication
  public hasInitialStatus: boolean;

  // Allow the use of enum in the component template
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    this.hasInitialStatus = true;
    this.isProfileComplete = true;
  }

  public routeTo(routePath: EnrolmentRoutes, isRelative: boolean = true) {
    const relativeTo = (isRelative) ? { relativeTo: this.route.parent } : null;
    this.router.navigate([routePath], relativeTo);
  }
}
