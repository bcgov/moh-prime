import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { exhaustMap } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

import { Site } from '@registration/shared/models/site.model';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolleeRemoteUser } from '@shared/models/enrollee-remote-user.model';



@Component({
  selector: 'app-remote-access',
  templateUrl: './remote-access.component.html',
  styleUrls: ['./remote-access.component.scss']
})
export class RemoteAccessComponent extends BaseEnrolmentProfilePage implements OnInit {
  public form: FormGroup;
  public sites: Site[] = [];
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
    private formBuilder: FormBuilder,
    private formUtilsService: FormUtilsService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger, utilService);
    this.enrolment = this.enrolmentService.enrolment;
  }

  public onClick() {
    this.hasNoSitesError = false;
    this.showProgress = true;
    this.siteResource.getSitesByRemoteUserInfo(this.enrolment.certifications)
      .pipe(
        exhaustMap((sites: Site[]) => {
          this.showProgress = false;
          if (sites.length === 0) {
            this.hasNoSitesError = true;
          }
          this.sites = sites;
          this.initForm();
          return this.sites;
        })
      ).subscribe(
        () => { },
        (error: any) => {
          this.showProgress = false;
          this.hasNoSitesError = true;
        }
      );
  }

  public checked(site: Site): boolean {
    let checked = false;
    site.remoteUsers.forEach((remoteUser) => {
      if (this.enrolment.enrolleeRemoteUsers?.find(eru => eru.remoteUserId === remoteUser.id)) {
        checked = true;
      }
    });
    return checked;
  }

  public showButton() {
    return this.sites.length === 0 && !this.showProgress && !this.hasNoSitesError;
  }

  public onSubmit() {
    const selectedSites = this.sites.map((site, i) => {
      if (this.form.controls.sites.value[i]) {
        return site;
      }
    });
    if (selectedSites.length > 0) {
      this.enrolmentResource
        .addEnrolleeRemoteUsers(this.enrolment.id, selectedSites)
        .subscribe(() => {
          this.nextRouteAfterSubmit();
        });
    } else {
      this.nextRouteAfterSubmit();
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.form = this.formBuilder.group({
      sites: new FormArray([])
    });
  }

  protected initForm() {
    this.form.controls.sites = this.formBuilder.array(this.sites.map(x => !1));
    console.log('before', this.form.controls.sites);
    this.sites.forEach((site, i) => {
      site.remoteUsers.forEach((remoteUser) => {
        if (this.enrolment.enrolleeRemoteUsers?.find(eru => eru.remoteUserId === remoteUser.id)) {
          this.form.controls.sites.value[i] = true;
        }
      });
    });
    console.log('after', this.form.controls.sites);
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

}
