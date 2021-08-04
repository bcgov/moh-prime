import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormGroupDirective } from '@angular/forms';
import { WeekDay } from '@angular/common';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { VendorEnum } from '@shared/enums/vendor.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { HoursOperationPageFormModel, HoursOperationPageFormState } from './hours-operation-page-form-state.class';

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
export class HoursOperationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: HoursOperationPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
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
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    route: ActivatedRoute,
    router: Router,
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
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

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
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
    // Force the site to be patched each time
    this.siteFormStateService.setForm(site, true);
    this.formState.form.markAsPristine();

    this.formState.businessDays.controls.forEach((group: FormGroup) => {
      if (this.is24Hours(group)) {
        this.allowEditingHours(group, false);
      }
    });
  }

  protected additionalValidityChecks(formValue: HoursOperationPageFormModel): boolean {
    return !!formValue.businessDays.length;
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoBusinessHoursError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoBusinessHoursError = true;
  }

  protected performSubmission(): NoContent {
    const payload = this.siteFormStateService.json;
    return this.siteResource.updateSite(payload)
      .pipe(tap(() => this.formState.form.markAsPristine()));
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.markAsPristine();

    const site = this.siteService.site;
    let routePath = SiteRoutes.REMOTE_USERS;

    if (site.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST || site.careSettingCode === CareSettingEnum.DEVICE_PROVIDER) {
      routePath = SiteRoutes.ADMINISTRATOR;
    } else if (this.isCompleted) {
      routePath = SiteRoutes.SITE_REVIEW;
    }

    this.routeUtils.routeRelativeTo(routePath);
  }

  private allowEditingHours(group: FormGroup, isEditable: boolean = true) {
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
