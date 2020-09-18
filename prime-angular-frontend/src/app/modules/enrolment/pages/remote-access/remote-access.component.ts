import { Component, OnInit, OnDestroy } from '@angular/core';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { SiteResource } from '@core/resources/site-resource.service';
import { Site } from '@registration/shared/models/site.model';
import { exhaustMap, delay } from 'rxjs/operators';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';

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
    private formBuilder: FormBuilder
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger, utilService);
  }

  public onClick() {
    this.hasNoSitesError = false;
    this.showProgress = true;
    const enrolment = this.enrolmentService.enrolment;
    this.siteResource.getSitesByRemoteUserInfo(enrolment.certifications)
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
      ).subscribe();
  }

  public showButton() {
    return this.sites.length === 0 && !this.showProgress && !this.hasNoSitesError;
  }

  public get sitesFormArray() {
    return this.form.controls.remoteAccessSites as FormArray;
  }

  public onSubmit() {
    alert('here');
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  protected createFormInstance() {
    this.form = this.formBuilder.group({
      sites: new FormArray([])
    });
  }

  protected initForm() {
    this.form.controls.sites = this.formBuilder.array(this.sites.map(x => !1));
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.CARE_SETTING;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

}
