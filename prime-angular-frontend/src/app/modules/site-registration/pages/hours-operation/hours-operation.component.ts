import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { BusinessDay } from '@lib/modules/business-hours/models/business-day.model';
import { UtilsService, SortWeight } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { WeekDay } from '@angular/common';

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

  public customPattern = {
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
    private formUtilsService: FormUtilsService,
    private utilsService: UtilsService,
    private dialog: MatDialog
  ) {
    this.title = 'Hours of Operation';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public get businessDays(): FormArray {
    return this.form.get('businessDays') as FormArray;
  }

  public onDayToggle(group: FormGroup, index: number) {
    if (this.hasDay(index)) {
      this.formUtilsService.resetAndClearValidators(group);
    } else {
      this.formUtilsService.setValidators(group, [Validators.required]);
      group.patchValue({ startTime: '0900', endTime: '1700' });
    }
  }

  public getWeekDay(num: number) {
    return WeekDay[num];
  }

  public hasDay(index: number): boolean {
    return (this.businessDays.value[index].startTime) ? true : false;
  }

  public onSubmit() {
    console.log(this.siteFormStateService.json);
    if (this.formUtilsService.checkValidity(this.businessDays)) {
      const payload = this.siteFormStateService.json;
      this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.ADMINISTRATOR);
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
  }

  private initForm() {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);
  }
}
