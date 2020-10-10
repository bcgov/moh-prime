import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl, Validators, FormGroupDirective } from '@angular/forms';
import { WeekDay } from '@angular/common';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { Subscription, Observable } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { VendorEnum } from '@shared/enums/vendor.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';

export class BusinessDayHoursErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: FormControl | null, form: FormGroupDirective | null): boolean {
    const invalidCtrl = super.isErrorState(control, form);
    // Apply custom validation from parent form group
    const dirtyOrSubmitted = (control?.dirty || form?.submitted);
    const invalidParent = !!(control?.parent && control?.parent.hasError('lessthan') && dirtyOrSubmitted);
    return (invalidCtrl || invalidParent);
  }
}

@Component({
  selector: 'app-hours-operation',
  templateUrl: './hours-operation.component.html',
  styleUrls: ['./hours-operation.component.scss']
})
export class HoursOperationComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public hasNoHours: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public WeekDay = WeekDay;
  public busDayHoursErrStateMatcher: BusinessDayHoursErrorStateMatcher;
  public hasNoBusinessHoursError: boolean;

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
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private dialog: MatDialog,
    public formUtilsService: FormUtilsService,
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public get businessDays(): FormArray {
    return this.form.get('businessDays') as FormArray;
  }

  public onSubmit() {
    const payload = this.siteFormStateService.json;
    if (this.formUtilsService.checkValidity(this.businessDays) && payload.businessHours.length) {
      this.hasNoBusinessHoursError = false;
      this.busy = this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      this.hasNoBusinessHoursError = true;
    }
  }

  public hasDay(group: FormGroup): boolean {
    return group.get('startTime').value !== null;
  }

  public is24Hours(group: FormGroup): boolean {
    return (
      group.get('startTime').value === this.business24Hours.startTime &&
      group.get('endTime').value === this.business24Hours.endTime
    ) ? true : false;
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

  public nextRoute() {
    const chosenVendorCode = this.siteService.site.siteVendors[0].vendorCode;
    if (
      chosenVendorCode === VendorEnum.CARECONNECT ||
      [ // Community pharmacy vendors
        VendorEnum.TELUS_HEALTH,
        VendorEnum.SHOPPERS_DRUG_MART,
        VendorEnum.APPLIED_ROBOTICS,
        VendorEnum.MCKESSON,
        VendorEnum.COMMANDER_GROUP
      ].includes(chosenVendorCode)
    ) {
      this.routeUtils.routeRelativeTo(SiteRoutes.ADMINISTRATOR);
    } else if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.REMOTE_USERS);
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.siteFormStateService.hoursOperationForm;
    this.busDayHoursErrStateMatcher = new BusinessDayHoursErrorStateMatcher();
  }

  private initForm() {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);

    this.businessDays.controls.forEach((group: FormGroup) => {
      if (this.is24Hours(group)) {
        this.allowEditingHours(group, false);
      }
    });

    this.form.markAsPristine();
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
