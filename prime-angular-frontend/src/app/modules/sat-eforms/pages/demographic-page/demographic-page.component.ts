import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';

import { iif, Observable, of } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Address, optionalAddressLineItems } from '@lib/models/address.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';
import { SatEformsEnrolmentResource } from '@sat/shared/resource/sat-eforms-enrolment-resource.service';
import { SatEnrolleeService } from '@sat/shared/services/sat-enrollee.service';
import { DemographicFormState } from './demographic-form-state.class';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';

// TODO create inheritable demographic class + mixins for reuse
@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: DemographicFormState;
  public enrollee: SatEnrollee;
  public routeUtils: RouteUtils;
  public readonly certificationsKey: string;
  /**
   * @description
   * User information from the provider not contained
   * within the form for use in creation.
   */
  public bcscUser: BcscUser;
  public hasPreferredName: boolean;
  public hasVerifiedAddress: boolean;
  public hasPhysicalAddress: boolean;


  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private fb: FormBuilder,
    private location: Location,
    private enrolmentResource: SatEformsEnrolmentResource,
    private enrolleeService: SatEnrolleeService,
    private authService: AuthService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
    this.certificationsKey = 'partyCertifications';
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getUser$()
      .pipe(
        map((bcscUser: BcscUser) => this.bcscUser = bcscUser),
        map(_ => this.patchForm())
      )
      .subscribe(() => this.initForm());
  }

  protected createFormInstance() {
    this.formState = new DemographicFormState(this.fb, this.configService, this.formUtilsService, this.certificationsKey);
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      // Don't throw an error as new enrolments are created in this view
      return;
    }

    this.enrollee = this.enrolleeService.enrollee;
    this.formState.patchValue(this.enrollee);
  }

  protected initForm() {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.formState.addEmptyCollegeCertification();
    }
  }

  protected performSubmission(): Observable<number> {
    const demographic = this.formState.json;
    const enrolleeId = +this.route.snapshot.params.eid;

    return this.getUser$()
      .pipe(
        map((user: BcscUser) => {
          // Ensure BCSC user information is never overwritten
          const { firstName, lastName, givenNames, ...remainder } = user;
          return { ...remainder, ...demographic, firstName, lastName, givenNames };
        }),
        exhaustMap((enrollee: SatEnrollee) =>
          (!enrolleeId)
            ? this.enrolmentResource.createSatEnrollee(enrollee)
              .pipe(
                map((enrollee: SatEnrollee) => {
                  // Replace the URL with redirection, and prevent initial
                  // ID of zero being pushed onto browser history
                  this.location.replaceState([SatEformsRoutes.MODULE_PATH, enrollee.id, SatEformsRoutes.DEMOGRAPHIC].join('/'));
                  return enrollee.id;
                })
              )
            : this.enrolmentResource.updateSatEnrollee(enrolleeId, enrollee)
              .pipe(map(() => enrolleeId))
        ),
        exhaustMap((enrolleeId: number) => {
          this.formState.removeIncompleteCertifications(true);
          return this.enrolmentResource.updateSatEnrolleeCertifications(enrolleeId, this.formState.json.partyCertifications)
            .pipe(
              exhaustMap(() => this.enrolmentResource.submitSatEnrollee(enrolleeId)),
              map(() => enrolleeId)
            );
        })
      );
  }

  protected afterSubmitIsSuccessful(enrolleeId: number): void {
    // Must go up a route-level and down with newly minted or existing
    // enrollee ID to override the replaced route state during submission
    this.routeUtils.routeRelativeTo(['../', enrolleeId, SatEformsRoutes.SUBMISSION_CONFIRMATION]);
  }

  /**
   * @description
   * Get an enrollee from a BCSC user with appropriate default
   * for properties that are not currently provided.
   */
  private getUser$(): Observable<BcscUser> {
    return this.authService.getUser$()
      .pipe(map((user: BcscUser) => ({ ...user, email: null })));
  }
}
