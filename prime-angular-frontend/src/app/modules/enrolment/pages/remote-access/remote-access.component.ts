import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { delay } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolleeRemoteAccessSite } from '@enrolment/shared/models/enrollee-remote-access.model';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

@Component({
  selector: 'app-remote-access',
  templateUrl: './remote-access.component.html',
  styleUrls: ['./remote-access.component.scss']
})
export class RemoteAccessComponent extends BaseEnrolmentProfilePage implements OnInit {
  @ViewChild('requestAccess') public requestAccess: MatSlideToggle;

  public form: FormGroup;
  public enrolment: Enrolment;
  public showProgress: boolean;
  public remoteSites: EnrolleeRemoteAccessSite[];
  public noRemoteSites: boolean;

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

  public get sites(): FormArray {
    return this.form.get('sites') as FormArray;
  }

  public get enrolleeRemoteUsers(): FormArray {
    return this.form.get('enrolleeRemoteUsers') as FormArray;
  }

  public onSubmit() {
    this.enrolleeRemoteUsers.clear();

    this.sites.controls.forEach((checked, i) => {
      if (checked.value) {
        this.remoteSites[i].remoteUsers.forEach(remoteUser => {
          const enrolleeRemoteUser = this.enrolmentFormStateService.enrolleeRemoteUserFormGroup();
          enrolleeRemoteUser.patchValue({ enrolleeId: this.enrolment.id, remoteUserId: remoteUser.id });
          this.enrolleeRemoteUsers.push(enrolleeRemoteUser);
        });
      }
    });

    super.onSubmit();
  }

  public onRequestAccess(event: MatSlideToggleChange) {
    if (event.checked) {
      this.getRemoteAccess();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();

    if (this.enrolment.enrolleeRemoteUsers.length) {
      this.getRemoteAccess();
    }
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.remoteAccessForm;
  }

  protected initForm() {
    this.form.controls.sites = this.fb.array(this.remoteSites.map(() => this.fb.control(false)));
    // Set already linked sites as checked
    const checked = [];
    this.remoteSites.forEach(remoteSite => {
      remoteSite.remoteUsers.forEach(remoteUser => {
        this.enrolleeRemoteUsers.controls.forEach(control => {
          if (control.get('remoteUserId').value === remoteUser.id) {
            checked.push(true);
          }
        });
      });
    });
    this.sites.patchValue(checked);
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = this.enrolleeRemoteUsers.length
        ? EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES
        : EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  /**
   * @description
   * Request remote access information.
   */
  private getRemoteAccess(): void {
    this.showProgress = true;
    this.noRemoteSites = false;
    this.siteResource.getSitesByRemoteUserInfo(this.enrolment.certifications)
      .pipe(delay(2000))
      .subscribe(
        (sites: EnrolleeRemoteAccessSite[]) => {
          if (sites.length) {
            this.noRemoteSites = false;
            this.remoteSites = sites;
            this.initForm();
          } else {
            this.noRemoteSites = true;
            this.requestAccess.checked = false;
          }
        },
        (error: any) => { },
        () => this.showProgress = false
      );
  }
}
