import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { FormUtilsService } from '@common/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.scss']
})
export class VendorComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  // TODO supply through config
  public vendorConfig: { id: number, name: string }[];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private siteRegistrationResource: SiteRegistrationResource,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    // TODO supply through config
    this.vendorConfig = [
      { id: 0, name: 'Excelleris' },
      { id: 1, name: 'iClinic Inc.' },
      { id: 2, name: 'Medinet' },
      { id: 3, name: 'Plexia Electronic Medical Systems' },
      { id: 4, name: 'CareConnect' }
    ];
  }

  public get vendors(): FormArray {
    return this.form.get('vendors') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      // this.siteRegistrationResource
      //   .updateSite(this.form.value)
      //   .subscribe(() => {
      this.form.markAsPristine();
      this.routeUtils.routeRelativeTo(SiteRoutes.HOURS_OPERATION);
      // });
    }
  }

  public onBack() {
    this.router.navigate([SiteRoutes.ORGANIZATION_AGREEMENT], { relativeTo: this.route.parent });
  }

  public onChange(name: string, isChecked: boolean) {
    (isChecked)
      ? this.vendors.push(new FormControl(name))
      : this.vendors.removeAt(this.vendors.controls.findIndex(c => c.value === name));
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
    this.form = this.fb.group({
      vendors: this.fb.array([])
    });
  }

  private initForm() {
    // TODO populate and pull from separate service with form models
  }
}
