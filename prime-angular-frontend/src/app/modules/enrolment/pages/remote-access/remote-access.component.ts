import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
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
  public sites: EnrolleeRemoteAccessSite[];
  public hasNoSitesError: boolean;
  public showProgress: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected siteResource: SiteResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService
    );
  }

  public get sitesFormArray(): FormArray {
    return this.form.get('sites') as FormArray;
  }

  public get enrolleeRemoteUsers(): FormArray {
    return this.form.get('enrolleeRemoteUsers') as FormArray;
  }

  public onSubmit() {
    this.sites.forEach(site => {
      site.remoteUsers.forEach(remoteUser => {
        const enrolleeRemoteUser = this.enrolmentFormStateService.enrolleeRemoteUserFormGroup();
        enrolleeRemoteUser.get('enrolleeId').setValue(this.enrolment.id);
        enrolleeRemoteUser.get('remoteUserId').setValue(remoteUser.id);
        this.enrolleeRemoteUsers.push(enrolleeRemoteUser);
      });
    });

    super.onSubmit();
  }

  public onRequestAccess() {
    this.hasNoSitesError = false;
    this.showProgress = true;
    this.siteResource.getSitesByRemoteUserInfo(this.enrolment.certifications)
      .subscribe(
        (sites: EnrolleeRemoteAccessSite[]) => {
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
    this.form = this.enrolmentFormStateService.remoteAccessForm;
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
      nextRoutePath = this.enrolleeRemoteUsers.length
        ? EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES
        : EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
