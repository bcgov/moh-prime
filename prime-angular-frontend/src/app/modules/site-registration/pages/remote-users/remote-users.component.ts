import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';

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
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
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
    // TODO structured to match in all site views
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO when spoking don't update
      const payload = this.siteFormStateService.site;
      this.siteResource
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
    this.form = this.siteFormStateService.remoteUsersForm;
  }

  private initForm() {
    // TODO structured to match in all site views
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // TODO cannot set form each time the view is loaded when updating
    // TODO temporary to prevent overwriting the parent form state
    const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';
    this.router.navigate([], { queryParams: { fromRemoteUser: null } });
    console.log('FROM_REMOTE_USER', !fromRemoteUser);

    this.siteFormStateService.setForm(site, !fromRemoteUser);
  }
}
