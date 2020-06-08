import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { Party } from '@registration/shared/models/party.model';
import { Address } from '@shared/models/address.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-technical-support',
  templateUrl: './technical-support.component.html',
  styleUrls: ['./technical-support.component.scss']
})
export class TechnicalSupportComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  private site: Site;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.title = 'Technical Support';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);
  }

  public onSubmit() {
    // TODO temporary fix for allow submissions of disabled forms
    const isDisabled = this.form.disabled;
    if (isDisabled) {
      this.form.enable();
    }
    // TODO structured to match in all site views
    if (this.formUtilsService.checkValidity(this.form)) {
      if (isDisabled) {
        this.form.disable();
      }
      // TODO when spoking don't update
      const payload = this.siteFormStateService.site;
      this.siteResource
        .updateSite(payload, true)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      if (isDisabled) {
        this.form.disable();
      }
    }
  }

  public onSelect(party: Party) {
    if (!party.physicalAddress) {
      party.physicalAddress = new Address();
    }
    this.form.patchValue(party);
    this.form.disable();
  }

  public onClear() {
    this.form.reset({
      id: 0,
      userId: '00000000-0000-0000-0000-000000000000'
    });
    this.form.enable();
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.PRIVACY_OFFICER);
  }

  public nextRoute() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
  }

  public isSameAs() {
    return this.site.provisioner.userId === this.site.location.technicalSupport?.userId ||
      this.site.provisioner.userId === this.form.get('userId').value;
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
    this.form = this.siteFormStateService.technicalSupportForm;
  }

  private initForm() {
    // TODO structured to match in all site views
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    // TODO cannot set form each time the view is loaded when updating
    this.siteFormStateService.setForm(this.site, true);

    // TODO temporary fix to disable same as party
    if (this.isSameAs()) {
      this.form.disable();
    }
  }
}
