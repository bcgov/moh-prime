import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

@Component({
  selector: 'app-remote-users',
  templateUrl: './remote-users.component.html',
  styleUrls: ['./remote-users.component.scss']
})
export class RemoteUsersComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private formUtilsService: FormUtilsService
  ) {
    this.title = 'Practitioners Requiring Remote PharmaNet Access';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get remoteUsers(): FormArray {
    return this.form.get('remoteUsers') as FormArray;
  }

  public get hasRemoteUsers(): FormControl {
    return this.form.get('hasRemoteUsers') as FormControl;
  }

  public onSubmit() {
    // TODO should we be saving remote users individually or as wholesale PUT?
    // TODO show validation message if hasRemoteUsers and remoteUsers is empty
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteRegistrationStateService.site;
      this.siteRegistrationResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onRemove(index: number) {
    this.remoteUsers.removeAt(index);
  }

  public onToggleRemoteUsers(change: MatSlideToggleChange) {
    // TODO now that it's in the form group refactor and add binding in template
    this.hasRemoteUsers.patchValue(change.checked);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.VENDOR]);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(['../', SiteRoutes.SITE_REVIEW]);
    } else {
      this.routeUtils.routeRelativeTo(['../', SiteRoutes.ADMINISTRATOR]);
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.siteRegistrationStateService.remoteUsersForm;
  }

  private initForm() {
    // TODO remove temporary route param to prevent setting the site
    const site = this.siteRegistrationService.site;
    this.isCompleted = site?.completed;
    this.siteRegistrationStateService.setSite(site, true);
  }
}
