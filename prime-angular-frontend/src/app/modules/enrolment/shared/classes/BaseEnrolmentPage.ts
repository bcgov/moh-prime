import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

export interface IBaseEnrolmentPage {
  busy: Subscription;
  isInitialEnrolment: boolean;
  isProfileComplete: boolean;
  EnrolmentRoutes: EnrolmentRoutes;
  routeTo(routePath: EnrolmentRoutes, navigationExtras: NavigationExtras): void;
}

export abstract class BaseEnrolmentPage implements IBaseEnrolmentPage {
  public busy: Subscription;

  // Whether this is the enrollee's initial enrolment, which indicates they
  // have never signed a terms of access agreement
  public isInitialEnrolment: boolean;

  // Whether the enrollee has completed filling in their initial enrolment, and
  // have made it to enrolment review after completion
  public isProfileComplete: boolean;

  // Allow the use of enum in the component template
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    // Defaults are for enrollees that have been approved at
    // least once, which represents the majority
    this.isInitialEnrolment = false;
    this.isProfileComplete = true;
  }

  public routeTo(routePath: EnrolmentRoutes, navigationExtras: NavigationExtras = {}) {
    this.router.navigate([routePath], {
      relativeTo: this.route.parent,
      ...navigationExtras
    });
  }
}
