import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { FormUtilsService } from '@common/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

@Component({
  selector: 'app-hours-operation',
  templateUrl: './hours-operation.component.html',
  styleUrls: ['./hours-operation.component.scss']
})
export class HoursOperationComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get hoursWeekend(): FormControl {
    return this.form.get('hoursWeekend') as FormControl;
  }

  public get hours24(): FormControl {
    return this.form.get('hours24') as FormControl;
  }

  public get hoursSpecial(): FormControl {
    return this.form.get('hoursSpecial') as FormControl;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteRegistrationStateService.site;
      this.siteRegistrationResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.routeUtils.routeRelativeTo(SiteRoutes.SIGNING_AUTHORITY);
        });
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.VENDORS);
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
    this.siteRegistrationStateService.setSite(site);
  }
}
