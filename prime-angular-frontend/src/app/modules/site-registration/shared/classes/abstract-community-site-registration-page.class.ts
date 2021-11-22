import { MatDialog } from '@angular/material/dialog';

import { noop, Observable, of } from 'rxjs';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { NoContentResponse } from '@core/resources/abstract-resource';

import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';

export abstract class AbstractCommunitySiteRegistrationPage<T extends AbstractFormState<unknown> = AbstractFormState<unknown>, S = unknown> extends AbstractEnrolmentPage {
  /**
   * @description
   * Form state
   */
  public abstract formState: T;

  protected constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource
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
  protected submissionRequest(): Observable<unknown> {
    const payload = this.siteFormStateService.json;
    return this.siteResource.updateSite(payload);
  }
}
