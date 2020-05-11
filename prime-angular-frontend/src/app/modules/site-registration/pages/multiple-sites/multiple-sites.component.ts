import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';


import { ToastService } from '@core/services/toast.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { FormUtilsService } from '@common/services/form-utils.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

// TODO who knows what this page is supposed to contribute... so for now it does nothing
@Component({
  selector: 'app-multiple-sites',
  templateUrl: './multiple-sites.component.html',
  styleUrls: ['./multiple-sites.component.scss']
})
export class MultipleSitesComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private toastService: ToastService,
    private formUtilsService: FormUtilsService,
    private formBuilder: FormBuilder,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
  }

  public get hasMultipleSites(): FormControl {
    return this.form.get('hasMultipleSites') as FormControl;
  }

  public get organizationNumber(): FormControl {
    return this.form.get('organizationNumber') as FormControl;
  }

  public onSubmit() {
    // TODO do nothing since this view makes no sense in the workflow
    this.form.markAsPristine();
    this.toastService.openSuccessToast('Site has been updated');
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_INFORMATION);
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
    this.form = this.formBuilder.group({
      hasMultipleSites: [
        { value: false, disabled: true },
        [FormControlValidators.requiredBoolean]
      ],
      organizationNumber: [null, []],
    });
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site?.completed;
    // this.siteRegistrationStateService.setSite(site, true);
    this.hasMultipleSites.valueChanges
      .subscribe((value: boolean) => this.toggleOrganizationValidators(value, this.organizationNumber));
  }

  private toggleOrganizationValidators(value: boolean, control: FormControl) {
    if (!value) {
      this.formUtilsService.resetAndClearValidators(control);
    } else {
      this.formUtilsService.setValidators(control, [Validators.required]);
    }
  }
}
