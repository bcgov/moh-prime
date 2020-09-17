import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl, Validators, FormGroupDirective } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { WeekDay } from '@angular/common';

import { Subscription, Observable } from 'rxjs';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { VendorEnum } from '@shared/enums/vendor.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
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

  public busyHoursTimePattern = {
    A: { pattern: new RegExp('\[0-2\]') },
    B: { pattern: new RegExp('\[0-9\]') },
    C: { pattern: new RegExp('\[0-5\]') },
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

  public onDayToggle(group: FormGroup, change: MatSlideToggleChange): void {
    if (change.checked) {
      this.hasNoBusinessHoursError = false;
    }

    if (this.hasDay(group)) {
      this.formUtilsService.resetAndClearValidators(group);
    } else {
      group.patchValue({ startTime: '0900', endTime: '1700' });
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
    if (this.siteService.site.siteVendors[0].vendorCode === VendorEnum.CARECONNECT) {
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
    this.form.markAsPristine();
  }
}
