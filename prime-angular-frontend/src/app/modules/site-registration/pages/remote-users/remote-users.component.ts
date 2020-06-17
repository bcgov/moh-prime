import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl } from '@angular/forms';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

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
  public hasNoRemoteUserError: boolean;

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
      this.hasNoRemoteUserError = false;
      // TODO when spoking don't update
      const payload = this.siteFormStateService.site;
      this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      this.hasNoRemoteUserError = true;
    }
  }

  public onRemove(index: number) {
    this.remoteUsers.removeAt(index);
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
    this.remoteUsers.valueChanges
      .subscribe((remoteUsers: RemoteUser[]) => {
        (remoteUsers.length)
          ? this.hasRemoteUsers.disable({ emitEvent: false })
          : this.hasRemoteUsers.enable({ emitEvent: false });
      });

    this.hasRemoteUsers.valueChanges
      .subscribe((hasRemoteUsers: boolean) => {
        (hasRemoteUsers)
          ? this.remoteUsers.setValidators(FormArrayValidators.atLeast(1))
          : this.remoteUsers.clearValidators();

        this.hasNoRemoteUserError = false;
        this.remoteUsers.updateValueAndValidity({ emitEvent: false });
      });

    // TODO structured to match in all site views
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // Inform the parent not to patch the form as there are outstanding changes
    // to the remote users that need to be persisted
    const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';
    // Remove query param from URL without refreshing
    this.router.navigate([], { queryParams: { fromRemoteUser: null } });
    this.siteFormStateService.setForm(site, !fromRemoteUser);
  }
}
