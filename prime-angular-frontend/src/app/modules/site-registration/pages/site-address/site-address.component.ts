import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { Country } from '@shared/enums/country.enum';
import { Province } from '@shared/enums/province.enum';
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
  selector: 'app-site-address',
  templateUrl: './site-address.component.html',
  styleUrls: ['./site-address.component.scss']
})
export class SiteAddressComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public formControlNames: string[];
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

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteRegistrationStateService.site;
      this.siteRegistrationResource
        .updateSite(this.form.value)
        .subscribe(() => {
          this.form.markAsPristine();
          this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_AGREEMENT);
        });
    }
  }

  public onBack() {
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
    this.form = this.siteRegistrationStateService.siteAddressForm;
  }

  private initForm() {
    // TODO populate and pull from separate service with form models
  }
}
