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
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { Site } from '@registration/shared/models/site.model';

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
  public remoteSites: Site[];
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

  // All sites returned from search that have remote users
  // with the same college licence as the enrollee used as checkboxes
  public get sites(): FormArray {
    return this.form.get('sites') as FormArray;
  }

  // Sites selected by the enrollee
  public get remoteAccessSites(): FormArray {
    return this.form.get('remoteAccessSites') as FormArray;
  }

  public get enrolleeRemoteUsers(): FormArray {
    return this.form.get('enrolleeRemoteUsers') as FormArray;
  }

  public onSubmit() {
    this.enrolleeRemoteUsers.clear();
    this.remoteAccessSites.clear();

    // for each checked site, add a enrolleeRemoteUser and a remoteAccessSite
    this.sites?.controls.forEach((checked, i) => {
      if (checked.value) {
        this.remoteSites[i].remoteUsers.forEach(remoteUser => {
          const enrolleeRemoteUser = this.enrolmentFormStateService.enrolleeRemoteUserFormGroup();
          enrolleeRemoteUser.patchValue({ enrolleeId: this.enrolment.id, remoteUserId: remoteUser.id });
          this.enrolleeRemoteUsers.push(enrolleeRemoteUser);

          const remoteAccessSite = this.enrolmentFormStateService.remoteAccessSiteFormGroup();
          remoteAccessSite.patchValue({
            enrolleeId: this.enrolment.id,
            siteId: this.remoteSites[i].id,
            doingBusinessAs: this.remoteSites[i].doingBusinessAs
          });
          this.remoteAccessSites.push(remoteAccessSite);
        });
      }
    });

    if (!this.enrolleeRemoteUsers.length) {
      this.removeRemoteAccessLocations();
    }

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
    } else if (this.enrolleeRemoteUsers.length) {
      nextRoutePath = EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES;
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
    this.siteResource.getSitesByRemoteUserInfo(this.enrolmentFormStateService.regulatoryForm.get('certifications').value)
      .pipe(delay(2000))
      .subscribe(
        (sites: Site[]) => {
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

  /**
   * @description
   * Remove remoteAccessLocations from the enrolment if no remote sites have been chosen
   */
  private removeRemoteAccessLocations() {
    const form = this.enrolmentFormStateService.remoteAccessLocationsForm;
    const locations = form.get('remoteAccessLocations') as FormArray;
    locations.clear();
  }
}
