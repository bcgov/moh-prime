import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormGroupDirective, FormBuilder, FormArray } from '@angular/forms';
import { WeekDay } from '@angular/common';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { BusinessDayHours } from '@lib/models/business-day-hours.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { HoursOperationFormState } from './hours-operation-form-state.class';

export class LessThanErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: FormControl | null, form: FormGroupDirective | null): boolean {
    const invalidCtrl = super.isErrorState(control, form);
    // Apply custom validation from parent form group
    const dirtyOrSubmitted = (control?.dirty || form?.submitted);
    const invalidParent = !!(control?.parent && control?.parent.hasError('lessthan') && dirtyOrSubmitted);
    return (invalidCtrl || invalidParent);
  }
}

@Component({
  selector: 'app-hours-operation-page',
  templateUrl: './hours-operation-page.component.html',
  styleUrls: ['./hours-operation-page.component.scss']
})
export class HoursOperationPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: HoursOperationFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasNoHours: boolean;
  public hasNoBusinessHoursError: boolean;
  public lessThanErrorStateMatcher: LessThanErrorStateMatcher;
  public WeekDay = WeekDay;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  public readonly businessHoursTimePattern = {
    A: { pattern: /[0-2]/ },
    B: { pattern: /[0-9]/ },
    C: { pattern: /[0-5]/ }
  };

  public readonly business24Hours = {
    startTime: '0000',
    endTime: '2400'
  };

  public readonly businessRegularHours = {
    startTime: '0900',
    endTime: '1700'
  };

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected healthAuthoritySiteService: HealthAuthoritySiteService,
    protected healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    protected healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private fb: FormBuilder,
    router: Router,
  ) {
    super(dialog, formUtilsService, route, healthAuthoritySiteService, healthAuthoritySiteFormStateService, healthAuthoritySiteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public hasDay(group: FormGroup): boolean {
    return group.get('startTime').value !== null;
  }

  public is24Hours(group: FormGroup): boolean {
    return (
      group.get('startTime').value === this.business24Hours.startTime &&
      group.get('endTime').value === this.business24Hours.endTime
    );
  }

  public on24Hours(change: MatCheckboxChange, group: FormGroup): void {
    (change.checked)
      ? group.patchValue(this.business24Hours)
      : group.patchValue(this.businessRegularHours);

    this.allowEditingHours(group, !change.checked);
  }

  public onDayToggle(group: FormGroup, change: MatSlideToggleChange): void {
    if (change.checked) {
      this.hasNoBusinessHoursError = false;
    }

    if (this.hasDay(group)) {
      this.formUtilsService.resetAndClearValidators(group);
    } else {
      group.patchValue(this.businessRegularHours);
      this.formUtilsService.setValidators(group, [
        Validators.required,
        FormControlValidators.requiredLength(4)
      ]);
    }
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.SITE_ADDRESS;

    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = this.healthAuthoritySiteFormStateService.hoursOperationFormState;
    this.lessThanErrorStateMatcher = new LessThanErrorStateMatcher();
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    const site = this.healthAuthoritySiteService.site;
    this.isCompleted = site?.completed;
    this.healthAuthoritySiteFormStateService.setForm(site, !this.hasBeenSubmitted);

    // TODO move this into form state, and perform individual, but expose for controller
    this.formState.businessDays.controls.forEach((group: FormGroup) => {
      if (this.is24Hours(group)) {
        this.allowEditingHours(group, false);
      }
    });
  }

  protected checkValidity(form: FormGroup | FormArray): boolean {
    // Form being disabled indicates that every day of the week is 24 hours
    return (form.disabled || this.formUtilsService.checkValidity(form)) && this.additionalValidityChecks(form.getRawValue());
  }

  protected additionalValidityChecks(formValue: { businessDays: BusinessDayHours[] }): boolean {
    // Ensure at least one business day has hours
    return !!formValue.businessDays.some(bd => bd.startTime);
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoBusinessHoursError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoBusinessHoursError = true;
  }

  protected afterSubmitIsSuccessful(): void {
    const nextRoutePath = (!this.isCompleted)
      ? HealthAuthSiteRegRoutes.REMOTE_USERS
      : HealthAuthSiteRegRoutes.SITE_OVERVIEW;

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }

  private allowEditingHours(group: FormGroup, isEditable: boolean = true): void {
    const startTime = group.get('startTime') as FormControl;
    const endTime = group.get('endTime') as FormControl;

    if (isEditable) {
      startTime.enable();
      endTime.enable();
    } else {
      startTime.disable();
      endTime.disable();
    }
  }
}
