import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { noop, Observable, of } from 'rxjs';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContentResponse } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';

// TODO matches AbstractCommunitySiteRegistrationPage until HealthAuthoritySiteRegistration is completed
//      then should be refactored for reuse using interfaces and abstractions
export abstract class AbstractHealthAuthoritySiteRegistrationPage<T extends AbstractFormState<unknown> = AbstractFormState<unknown>, S = unknown> extends AbstractEnrolmentPage {
  protected constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthorityFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource
  ) {
    super(dialog, formUtilsService);
  }

  /**
   * @description
   * Whether the site has been submitted indicating this is
   * the initial creation of a site or an existing site.
   */
  public get hasBeenSubmitted(): boolean {
    return !!this.healthAuthoritySiteService.site?.submittedDate;
  }

  /**
   * @description
   * Deactivation guard hook to allow for specific actions
   * to be performed based on user interaction.
   *
   * NOTE: Usage example would be replacing previous form
   * values on deactivation so updates are discarded.
   */
  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    this.formState.patchValue(this.healthAuthoritySiteService.site);
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
    const payload = this.healthAuthorityFormStateService.json.forUpdate();

    return this.healthAuthoritySiteResource.updateHealthAuthoritySite(haid, sid, payload);
  }
}
