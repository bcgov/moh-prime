import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable, pipe } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { optionalAddressLineItems } from '@shared/models/address.model';

import { DemographicFormState } from './demographic-form-state.class';
import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import moment from 'moment';
import { MINIMUM_AGE } from '@lib/constants';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { RouteUtils } from '@lib/utils/route-utils.class';

@Component({
  selector: 'app-demographic',
  templateUrl: './demographic.component.html',
  styleUrls: ['./demographic.component.scss']
})
export class DemographicComponent extends BaseEnrolmentPage implements OnInit {
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
    super(
      route,
      router
    );

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public get firstName(): FormControl {
    return this.form.get('firstName') as FormControl;
  }

  public get middleName(): FormControl {
    return this.form.get('middleName') as FormControl;
  }

  public get lastName(): FormControl {
    return this.form.get('lastName') as FormControl;
  }

  public get dateOfBirth(): FormControl {
    return this.form.get('dateOfBirth') as FormControl;
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public onSubmit(): void {
    this.nextRouteAfterSubmit();

    // if (this.formUtilsService.checkValidity(this.form)) {
    //   this.handleSubmission();
    // } else {
    //   this.utilService.scrollToErrorSection();
    // }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm()
  }

  private createFormInstance(): void {
    this.formState = this.paperEnrolmentFormStateService.demographicFormState;
    this.form = this.formState.form;
  }


  /**
   * @description
   * Patch the form with enrollee information.
   */
  private patchForm(): void {
    // Will be null if enrolment has not been created
    const enrolment = this.paperEnrolmentService.enrolment;
    this.paperEnrolmentFormStateService.setForm(enrolment);
  }

  private handleSubmission() {
    // Update using the form which could contain changes, and ensure identity
    const enrolment = this.paperEnrolmentFormStateService.json;
    this.busy = this.performHttpRequest(enrolment).subscribe();
  }

  private performHttpRequest(enrolment: Enrolment): Observable<void> {
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

  /**
   * @description
   * Generic handler for the HTTP response. By default this covers update, and can
   * also be used for create actions, or extended for any response.
   */
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
