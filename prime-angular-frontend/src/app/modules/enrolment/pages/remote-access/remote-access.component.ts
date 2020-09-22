import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { exhaustMap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { Enrolment } from '@shared/models/enrolment.model';

import { Site } from '@registration/shared/models/site.model';

import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';


@Component({
  selector: 'app-remote-access',
  templateUrl: './remote-access.component.html',
  styleUrls: ['./remote-access.component.scss']
})
export class RemoteAccessComponent extends BaseEnrolmentProfilePage implements OnInit {
  public form: FormGroup;
  public sites: Site[];
  public hasNoSitesError: boolean;
  public showProgress: boolean;
  public enrolment: Enrolment;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected siteResource: SiteResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private fb: FormBuilder
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger, utilService);
    this.enrolment = this.enrolmentService.enrolment;
  }

  public get sitesFormArray(): FormArray {
    return this.form.get('sites') as FormArray;
  }

  public onSubmit() {
    const selectedSites = this.sites.filter((site, i) => this.sitesFormArray.value[i]);

    this.busy = this.enrolmentResource
      .createEnrolleeRemoteUsers(this.enrolment.id, selectedSites)
      .subscribe(() =>
        this.nextRouteAfterSubmit()
      );
  }

  public onRequestAccess() {
    this.hasNoSitesError = false;
    this.showProgress = true;
    this.siteResource.getSitesByRemoteUserInfo(this.enrolment.certifications)
      .subscribe(
        (sites: Site[]) => {
          this.showProgress = false;
          if (!sites.length) {
            this.hasNoSitesError = true;
          }
          this.sites = sites;
          this.initForm();
        },
        (error: any) => {
          this.showProgress = false;
          this.hasNoSitesError = true;
        }
      );
  }

  public showRequestAccess() {
    return !this.sites?.length && !this.showProgress && !this.hasNoSitesError;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      sites: this.fb.array([])
    });
  }

  protected initForm() {
    this.form.controls.sites = this.fb.array(this.sites.map(() => this.fb.control(false)));
    // Set already linked sites as checked
    const checked = [];
    this.sites.forEach((site) => {
      site.remoteUsers.forEach((remoteUser) =>
        checked.push((this.enrolment.enrolleeRemoteUsers?.some(eru => eru.remoteUserId === remoteUser.id)))
      );
    });
    this.sitesFormArray.patchValue(checked);
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
