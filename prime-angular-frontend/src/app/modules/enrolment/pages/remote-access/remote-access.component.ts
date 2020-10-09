import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { delay } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolleeRemoteAccessSite } from '@enrolment/shared/models/enrollee-remote-access.model';

@Component({
  selector: 'app-remote-access',
  templateUrl: './remote-access.component.html',
  styleUrls: ['./remote-access.component.scss']
})
export class RemoteAccessComponent extends BaseEnrolmentProfilePage implements OnInit {
  public form: FormGroup;
  public enrolment: Enrolment;
  public remoteSites: EnrolleeRemoteAccessSite[];
  public showProgress: boolean;

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
    if (this.remoteSites?.length) {
      const selectedSites = this.remoteSites
        .filter((site, i) => this.sitesFormArray.value[i])
        .map(site => site.id);

      this.busy = this.enrolmentResource
        .createEnrolleeRemoteUsers(this.enrolment.id, selectedSites)
        .subscribe(() => this.nextRouteAfterSubmit());
    } else {
      this.nextRouteAfterSubmit();
    }
  }

  public onRequestAccess(event: MatSlideToggleChange) {
    if (event.checked) {
      this.showProgress = true;
      this.siteResource.getSitesByRemoteUserInfo(this.enrolment.certifications)
        .pipe(delay(2000))
        .subscribe(
          (sites: EnrolleeRemoteAccessSite[]) => {
            this.remoteSites = sites;
            this.initForm();
          },
          (error: any) => { },
          () => this.showProgress = false
        );
    }
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
    this.form.controls.sites = this.fb.array(this.remoteSites.map(() => this.fb.control(false)));
    // Set already linked sites as checked
    const checked = [];
    this.remoteSites.forEach(remoteSite =>
      remoteSite.remoteUsers.forEach(remoteUser =>
        checked.push(this.enrolment.enrolleeRemoteUsers?.some(eru => eru.remoteUserId === remoteUser.id))
      )
    );
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
