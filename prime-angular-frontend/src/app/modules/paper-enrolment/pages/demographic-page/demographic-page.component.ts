import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { pipe } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { optionalAddressLineItems } from '@shared/models/address.model';

import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicFormState } from './demographic-page-form-state.class';

@Component({
  selector: 'app-demographic',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public form: FormGroup;
  public formState: DemographicFormState;
  public enrolment: Enrolment;
  public maxDateOfBirth: moment.Moment;
  public routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected paperEnrolmentService: PaperEnrolmentService,
    protected paperEnrolmentResource: PaperEnrolmentResource,
    protected paperEnrolmentFormStateService: PaperEnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
  ) {
    super(dialog, formUtilsService);

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onSubmit(): void {
    this.nextRouteAfterSubmit();

    // if (this.formUtilsService.checkValidity(this.form)) {
    //   this.performSubmission();
    // } else {
    //   this.utilService.scrollToErrorSection();
    // }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = this.paperEnrolmentFormStateService.demographicFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    // Will be null if enrolment has not been created
    const enrolment = this.paperEnrolmentService.enrolment;
    this.paperEnrolmentFormStateService.setForm(enrolment);
  }

  protected performSubmission(): NoContent {
    // Update using the form which could contain changes, and ensure identity
    const enrolment = this.paperEnrolmentFormStateService.json;
    const enrollee = this.form.getRawValue();
    // BCeID has to match BCSC for submission, which requires givenNames
    const givenNames = `${enrollee.firstName} ${enrollee.middleName}`;

    if (!enrolment.id) {
      const payload = {
        enrollee: { ...enrollee, givenNames }
      };
      return this.paperEnrolmentResource.createEnrollee(payload)
        .pipe(
          // Merge the enrolment with generated keys
          map((newEnrolment: Enrolment) => {
            newEnrolment.enrollee = { ...newEnrolment.enrollee, ...enrolment.enrollee };
            return newEnrolment;
          }),
          // Populate generated keys within the form state
          tap((newEnrolment: Enrolment) => {
            this.paperEnrolmentFormStateService.setForm(newEnrolment, true);
            this.enrolment = newEnrolment;
          }),
          this.handleResponse()
        );
    } else {
      enrolment.enrollee.givenNames = givenNames;
      return this.paperEnrolmentResource.updateEnrollee(enrolment)
        .pipe(this.handleResponse());
    }
  }

  private handleResponse() {
    return pipe(
      map(() => {
        this.toastService.openSuccessToast('Enrolment information has been saved');
        this.form.markAsPristine();

        this.nextRouteAfterSubmit();
      }),
      catchError((error: any) => {
        this.toastService.openErrorToast('Enrolment information could not be saved');
        this.logger.error('[Enrolment] Submission error has occurred: ', error);

        throw error;
      })
    );
  }

  private nextRouteAfterSubmit(): void {
    // this.routeTo(['../', this.enrolment.id, PaperEnrolmentRoutes.CARE_SETTING]);
    this.routeUtils.routeRelativeTo(['../', '1', PaperEnrolmentRoutes.CARE_SETTING]);
  }

  private setAddressValidator(addressLine: FormGroup): void {
    this.formUtilsService.setValidators(addressLine, [Validators.required], optionalAddressLineItems);
  }
}
