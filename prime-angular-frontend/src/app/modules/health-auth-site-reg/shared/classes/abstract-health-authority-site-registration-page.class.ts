import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { noop, Observable, of } from 'rxjs';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthorityFormStateService } from '@health-auth/shared/services/health-authority-form-state.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { SiteService } from '@registration/shared/services/site.service';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';

// TODO matches AbstractCommunitySiteRegistrationPage until HealthAuthoritySiteRegistration is completed
//      then should be refactored for reuse
export abstract class AbstractHealthAuthoritySiteRegistrationPage<T extends AbstractFormState<unknown> = AbstractFormState<unknown>, S = unknown> extends AbstractEnrolmentPage {
  protected constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected siteService: HealthAuthoritySiteService,
    protected formStateService: HealthAuthorityFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource
  ) {
    super(dialog, formUtilsService);
  }

  /**
   * @description
   * Whether the site has been submitted indicating this is
   * the initial creation of a site or an existing site.
   */
  public get hasBeenSubmitted(): boolean {
    return !!this.siteService.site?.submittedDate;
  }

  /**
   * @description
   * Submission hook for execution.
   */
  protected performSubmission(): Observable<unknown | void> {
    if (!this.hasBeenSubmitted) {
      return this.submissionRequest();
    }

    // Updates only occur on submission after an
    // initial submission has been performed
    return of(noop()).pipe(NoContentResponse);
  }

  /**
   * @description
   * Request performed on submission when site is initially
   * being created and is not submitted.
   *
   * NOTE: Most pages will use a generic update, but
   * a few need to expand on the performed request.
   */
  protected submissionRequest(): Observable<unknown | void> {
    const { haid, sid } = this.route.snapshot.params;
    const payload = this.formStateService.json.forUpdate();

    return this.healthAuthorityResource.updateHealthAuthoritySite(haid, sid, payload);
  }
}
