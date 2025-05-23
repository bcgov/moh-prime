import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';
import { Address } from '@lib/models/address.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/shared/services/paper-enrolment-resource.service';
import { DemographicFormState } from './demographic-form-state.class';

@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: DemographicFormState;
  public maxDateOfBirth: moment.Moment;

  private enrollee: HttpEnrollee | null;
  private routeUtils: RouteUtils;

  public forceShowAddressFieldsIfEmpty: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: UntypedFormBuilder,
    private location: Location,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
    this.forceShowAddressFieldsIfEmpty = false;
  }

  public showAddressFields(address: Address): boolean {
    return Address.isNotEmpty(address) || this.forceShowAddressFieldsIfEmpty;
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
      // Don't throw an error as new enrolments are created in this view
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        if (enrollee) {
          this.enrollee = enrollee;

          const {
            firstName,
            givenNames,
            lastName,
            dateOfBirth,
            physicalAddress,
            additionalAddresses,
            email,
            phone,
            phoneExtension,
            smsPhone
          } = enrollee;

          // Attempt to patch the form if not already patched
          this.formState.patchValue({
            firstName,
            givenNames,
            lastName,
            dateOfBirth,
            physicalAddress,
            additionalAddresses,
            email,
            phone,
            phoneExtension,
            smsPhone
          });
        }
      });
  }

  protected performSubmission(): Observable<HttpEnrollee> {
    const payload = this.formState.json;
    const enrolleeId = +this.route.snapshot.params.eid;
    let request$ = this.paperEnrolmentResource.updateDemographic(enrolleeId, payload)
      .pipe(map(() => this.enrollee));

    if (!enrolleeId) {
      request$ = this.paperEnrolmentResource.createEnrollee(payload)
        .pipe(
          map((enrollee: HttpEnrollee) => {
            // Replace the URL with redirection, and prevent initial
            // ID of zero being pushed onto browser history
            this.location.replaceState([PaperEnrolmentRoutes.MODULE_PATH, enrollee.id, PaperEnrolmentRoutes.DEMOGRAPHIC].join('/'));
            return enrollee;
          })
        );
    }

    return request$;
  }

  protected afterSubmitIsSuccessful(enrollee: HttpEnrollee): void {
    const nextRoutePath = (enrollee.profileCompleted)
      ? PaperEnrolmentRoutes.OVERVIEW
      // Must go up a route-level and down with newly minted enrollee ID
      // to override the replaced route state during submission
      : ['../', enrollee.id, PaperEnrolmentRoutes.CARE_SETTING];

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }

  protected onSubmitFormIsInvalid(): void {
    this.forceShowAddressFieldsIfEmpty = true;
  }
}
