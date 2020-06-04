import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-vendor',
  templateUrl: './vendor.component.html',
  styleUrls: ['./vendor.component.scss']
})
export class VendorComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  // TODO supply through config
  public vendorConfig: { id: number, name: string }[];
  public hasNoVendorError: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.title = 'What PharmaNet software vendor does this site use?';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);

    // TODO supply through config using lookups
    this.vendorConfig = [
      { id: 1, name: 'CareConnect' },
      { id: 2, name: 'Excelleris' },
      { id: 3, name: 'iClinic Inc.' },
      { id: 4, name: 'Medinet' },
      { id: 5, name: 'Plexia Electronic Medical Systems' }
    ];
    // TODO should be an autocomplete instead of radio buttons to scale when there are more vendors
    this.hasNoVendorError = false;
  }

  public get vendorId(): FormControl {
    return this.form.get('id') as FormControl;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO when spoking don't update
      const payload = this.siteFormStateService.site;
      this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      this.hasNoVendorError = true;
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.HOURS_OPERATION);
  }

  public onChange() {
    this.hasNoVendorError = false;
  }

  public nextRoute() {
    if (this.isCompleted) {
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
    this.form = this.siteFormStateService.vendorForm;
  }

  private initForm() {
    // TODO structured to match in all site views
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // TODO cannot set form each time the view is loaded when updating
    this.siteFormStateService.setForm(site, true);
  }
}
