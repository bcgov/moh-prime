import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { optionalAddressLineItems } from '@shared/models/address.model';

import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicFormState } from './demographic-form-state.class';

@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: DemographicFormState;
  public maxDateOfBirth: moment.Moment;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private paperEnrolmentService: PaperEnrolmentService,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private paperEnrolmentFormStateService: PaperEnrolmentFormStateService,
    private toastService: ToastService,
    private logger: LoggerService,
    private utilService: UtilsService,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new DemographicFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(): void {
    // Will be null if enrollee has not been created
    const enrollee = this.paperEnrolmentService.enrollee;
    if (enrollee) {
      const {
        firstName,
        givenNames,
        lastName,
        dateOfBirth,
        physicalAddress,
        email,
        phone,
        phoneExtension,
        smsPhone
      } = enrollee;

      const middleName = givenNames.replace(firstName, '').trim();

      // Attempt to patch the form if not already patched
      this.formState.patchValue({
        firstName,
        middleName,
        lastName,
        dateOfBirth,
        physicalAddress,
        email,
        phone,
        phoneExtension,
        smsPhone
      });
    }
  }

  protected performSubmission(): NoContent {
    const payload = this.formState.json;
    const enrolleeId = this.paperEnrolmentService.enrollee?.id ?? 0;
    let request$ = this.paperEnrolmentResource.updateDemographic(enrolleeId, payload);

    if (!enrolleeId) {
      request$ = this.paperEnrolmentResource.createEnrollee(payload)
        .pipe(NoContentResponse);
    }

    return request$;
  }

  protected afterSubmitIsSuccessful() {
    this.routeUtils.routeRelativeTo(['./', '1', PaperEnrolmentRoutes.CARE_SETTING]);
  }

  // private setAddressValidator(addressLine: FormGroup): void {
  //   this.formUtilsService.setValidators(addressLine, [Validators.required], optionalAddressLineItems);
  // }
}
