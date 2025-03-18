import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormGroup, UntypedFormControl, Validators, FormGroupDirective, UntypedFormArray } from '@angular/forms';
import { WeekDay } from '@angular/common';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { HoursOperationPageFormModel, HoursOperationPageFormState } from './hours-operation-page-form-state.class';

export class LessThanErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: UntypedFormControl | null, form: FormGroupDirective | null): boolean {
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
export class HoursOperationPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: HoursOperationPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public isSubmitted: boolean;
  public hasNoHours: boolean;
  public hasNoBusinessHoursError: boolean;
  public lessThanErrorStateMatcher: LessThanErrorStateMatcher;
  public WeekDay = WeekDay;
  public SiteRoutes = SiteRoutes;

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
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    route: ActivatedRoute,
    router: Router,
  ) {
    super(dialog, formUtilsService, siteService, siteFormStateService, siteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public hasDay(group: UntypedFormGroup): boolean {
    return group.get('startTime').value !== null;
  }

  public is24Hours(group: UntypedFormGroup): boolean {
    return (
      group.get('startTime').value === this.business24Hours.startTime &&
      group.get('endTime').value === this.business24Hours.endTime
    );
  }

  public on24Hours(change: MatCheckboxChange, group: UntypedFormGroup): void {
    (change.checked)
      ? group.patchValue(this.business24Hours)
      : group.patchValue(this.businessRegularHours);

    this.allowEditingHours(group, !change.checked);
  }

  public onDayToggle(group: UntypedFormGroup, change: MatSlideToggleChange): void {
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

  public onBack() {
    const nextRoute = (!this.isCompleted)
      ? SiteRoutes.BUSINESS_LICENCE
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(nextRoute);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.hoursOperationPageFormState;
    this.lessThanErrorStateMatcher = new LessThanErrorStateMatcher();
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.isSubmitted = site?.submittedDate ? true : false;
    this.siteFormStateService.setForm(site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();

    this.formState.businessDays.controls.forEach((group: UntypedFormGroup) => {
      if (this.is24Hours(group)) {
        this.allowEditingHours(group, false);
      }
    });
  }

  protected checkValidity(form: UntypedFormGroup | UntypedFormArray): boolean {
    // Form being disabled indicates that every day of the week is 24 hours
    return (form.disabled || this.formUtilsService.checkValidity(form)) && this.additionalValidityChecks(form.getRawValue());
  }

  protected additionalValidityChecks(formValue: HoursOperationPageFormModel): boolean {
    return !!this.siteFormStateService.json.businessHours.length;
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoBusinessHoursError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoBusinessHoursError = true;
  }

  protected afterSubmitIsSuccessful(): void {
    const site = this.siteService.site;
    let routePath;

    if (this.isCompleted) {
      routePath = SiteRoutes.SITE_REVIEW;
    } else {
      switch (site.careSettingCode) {
        case CareSettingEnum.COMMUNITY_PHARMACIST:
        case CareSettingEnum.DEVICE_PROVIDER:
          routePath = SiteRoutes.ADMINISTRATOR;
          break;
        case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
          routePath = SiteRoutes.REMOTE_USERS;
          break;
        default:
          routePath = SiteRoutes.ADMINISTRATOR;
          break;
      }
    }

    this.routeUtils.routeRelativeTo(routePath);
  }

  private allowEditingHours(group: UntypedFormGroup, isEditable: boolean = true) {
    const startTime = group.get('startTime') as UntypedFormControl;
    const endTime = group.get('endTime') as UntypedFormControl;

    if (isEditable) {
      startTime.enable();
      endTime.enable();
    } else {
      startTime.disable();
      endTime.disable();
    }
  }
}
