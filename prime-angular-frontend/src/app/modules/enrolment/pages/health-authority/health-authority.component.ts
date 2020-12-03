import { Component, OnInit } from '@angular/core';
import { FormArray, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { FacilityEnum } from '@shared/enums/facility.enum';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

@Component({
  selector: 'app-health-authority',
  templateUrl: './health-authority.component.html',
  styleUrls: ['./health-authority.component.scss']
})
export class HealthAuthorityComponent extends BaseEnrolmentProfilePage implements OnInit {
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
    protected formUtilsService: FormUtilsService
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
      formUtilsService
    );
  }

  public get enrolleeHealthAuthorities(): FormArray {
    return this.form.get('enrolleeHealthAuthorities') as FormArray;
  }

  public getFacilities(formGroup: FormGroup): FormArray {
    return formGroup.get('facilities') as FormArray;
  }

  public routeBackTo() {
    // TODO setup expected back route based on enrolment
    this.routeTo(EnrolmentRoutes.CARE_SETTING);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.healthAuthoritiesForm;
  }

  protected initForm() {
    this.enrolleeHealthAuthorities.controls.forEach((group: FormGroup) => {
      const array = this.getFacilities(group);
      group.valueChanges.subscribe(value => {
        if (value.checked) {
          this.hasNoHealthAuthorityError = false;
          array.setValidators(FormArrayValidators.atLeast(1, c => !Validators.requiredTrue(c.get('checked'))));
        } else {
          array.clearValidators();
        }
      });
    });
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
    // TODO setup expected next route based on enrolment
    // super.nextRouteAfterSubmit();
  }
}
