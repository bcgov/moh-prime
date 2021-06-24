import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { Address } from '@shared/models/address.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { DemographicFormState } from './demographic-form-state.class';

@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: DemographicFormState;
  public maxDateOfBirth: moment.Moment;
  public showAddressFields: boolean;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private location: Location,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
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
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
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

          if (Address.isNotEmpty(physicalAddress)) {
            this.showAddressFields = true;
          }
        }
      });
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    const payload = this.formState.json;
    const enrolleeId = +this.route.snapshot.params.eid;
    let request$ = this.paperEnrolmentResource.updateDemographic(enrolleeId, payload)
      .pipe(map(() => enrolleeId));

    if (!enrolleeId) {
      request$ = this.paperEnrolmentResource.createEnrollee(payload)
        .pipe(
          map((enrollee: HttpEnrollee) => {
            // Replace the URL with redirection, and prevent initial
            // ID of zero being pushed onto browser history
            this.location.replaceState([PaperEnrolmentRoutes.MODULE_PATH, enrollee.id, PaperEnrolmentRoutes.DEMOGRAPHIC].join('/'));
            return enrollee.id;
          })
        );
    }

    return request$;
  }

  protected afterSubmitIsSuccessful(enrolleeId: number): void {
    this.routeUtils.routeRelativeTo(['../', enrolleeId, PaperEnrolmentRoutes.CARE_SETTING]);
  }

  protected onSubmitFormIsInvalid(): void {
    this.showAddressFields = true;
  }
}
