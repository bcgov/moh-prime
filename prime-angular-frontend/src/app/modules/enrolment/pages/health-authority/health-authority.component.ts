import { Component, OnInit } from '@angular/core';
import { FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConfigService } from '@config/config.service';
import { Config } from '@config/config.model';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { FacilityEnum } from '@shared/enums/facility.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

@Component({
  selector: 'app-health-authority',
  templateUrl: './health-authority.component.html',
  styleUrls: ['./health-authority.component.scss']
})
export class HealthAuthorityComponent extends BaseEnrolmentProfilePage implements OnInit {
  public facilities: Config<number>[];
  public hasNoHealthAuthorityError: boolean;
  public HealthAuthorityEnum = HealthAuthorityEnum;
  public FacilityEnum = FacilityEnum;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService,
    private configService: ConfigService
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService,
      authService
    );

    this.facilities = this.configService.facilities;
  }

  public get enrolleeHealthAuthorities(): FormArray {
    return this.form.get('enrolleeHealthAuthorities') as FormArray;
  }

  public routeBackTo() {
    this.routeTo(EnrolmentRoutes.CARE_SETTING);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  protected createFormInstance() {
    this.formState = this.enrolmentFormStateService.healthAuthoritiesFormState;
    this.form = this.formState.form;
  }

  protected initForm() {
    this.enrolleeHealthAuthorities.valueChanges.subscribe(() => {
      this.hasNoHealthAuthorityError = false;
    });

    this.enrolmentFormStateService.healthAuthoritiesFormState.setValidators();
  }

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsValid(): void {
    this.hasNoHealthAuthorityError = false;
  }

  /**
   * @description
   * Pre-submission hook for execution.
   */
  protected onSubmitFormIsInvalid(): void {
    this.hasNoHealthAuthorityError = true;
  }

  protected nextRouteAfterSubmit() {
    const oboSites = this.enrolmentFormStateService.jobsForm.get('oboSites').value as OboSite[];

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.REGULATORY;
    } else if (oboSites?.length) {
      nextRoutePath = EnrolmentRoutes.JOB;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
