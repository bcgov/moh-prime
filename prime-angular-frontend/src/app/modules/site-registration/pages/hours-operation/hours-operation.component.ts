import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { FormUtilsService } from '@common/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { BusinessDay } from '@registration/shared/models/business-day.model';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { UtilsService, SortWeight } from '@core/services/utils.service';

@Component({
  selector: 'app-hours-operation',
  templateUrl: './hours-operation.component.html',
  styleUrls: ['./hours-operation.component.scss']
})
export class HoursOperationComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private formUtilsService: FormUtilsService,
    private utilsService: UtilsService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get businessDays(): FormArray {
    return this.form.get('businessDays') as FormArray;
  }

  public onSubmit() {
    // TODO handle validations
    console.log('SUBMIT', this.form.getRawValue());

    if (this.formUtilsService.checkValidity(this.businessDays)) {
      console.log('VALID');
      //   const payload = this.siteRegistrationStateService.site;
      //   this.siteRegistrationResource
      //     .updateSite(payload)
      //     .subscribe(() => {
      //       this.form.markAsPristine();
      //       this.nextRoute();
      //     });
    }
  }

  public onAdd(businessDay: BusinessDay[]) {
    this.formUtilsService.formArrayPush(this.businessDays, businessDay);
    const sorted = this.businessDays.value.sort(this.sortConfigByDay());
    this.businessDays.patchValue(sorted);
  }

  public onRemove(index: number) {
    this.businessDays.removeAt(index);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.VENDOR);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.SIGNING_AUTHORITY);
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
    this.form = this.siteRegistrationStateService.hoursOperationForm;
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site?.completed;
    this.siteRegistrationStateService.setSite(site, true);
  }

  /**
   * @description
   * Sort by day of the week.
   */
  private sortConfigByDay(): (a: BusinessDay, b: BusinessDay) => SortWeight {
    return (a: BusinessDay, b: BusinessDay) =>
      this.utilsService.sort<BusinessDay>(a, b, 'day');
  }
}
