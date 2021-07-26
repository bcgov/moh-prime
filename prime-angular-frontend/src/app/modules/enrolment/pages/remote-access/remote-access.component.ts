import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import { delay } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { RemoteAccessSearch } from '@enrolment/shared/models/remote-access-search.model';
import { CertSearch } from '@enrolment/shared/models/cert-search.model';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-remote-access',
  templateUrl: './remote-access.component.html',
  styleUrls: ['./remote-access.component.scss']
})
export class RemoteAccessComponent extends BaseEnrolmentProfilePage implements OnInit {
  @ViewChild('requestAccess') public requestAccess: MatSlideToggle;

  public form: FormGroup;
  public showProgress: boolean;
  public remoteAccessSearch: RemoteAccessSearch[];
  public noRemoteAccess: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected siteResource: SiteResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService,
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
      formUtilsService,
      authService
    );
  }

  /**
   * @description
   * Site search results that had a remote user with the same college
   * licence as the enrollee.
   *
   * @usage
   * Used with the checkboxes to indicate remote access where if
   * checked is used to create the submission payload.
   */
  public get sites(): FormArray {
    return this.form.get('sites') as FormArray;
  }

  /**
   * @description
   * Sites that were recently selected by the enrollee.
   */
  public get remoteAccessSites(): FormArray {
    return this.form.get('remoteAccessSites') as FormArray;
  }

  /**
   * @description
   * Enrollee remote users constructed from
   */
  public get enrolleeRemoteUsers(): FormArray {
    return this.form.get('enrolleeRemoteUsers') as FormArray;
  }

  public onSubmit() {
    // Out with the old, and in with the new!
    this.enrolleeRemoteUsers.clear();
    this.remoteAccessSites.clear();

    const enrolleeId = this.enrolment.id;

    // Any checked sites are converted into an enrollee remote user, and
    // remote access site forming the submission payload
    this.sites?.controls.forEach((checked, i) => {
      if (checked.value) {
        const ras = this.remoteAccessSearch[i];
        const enrolleeRemoteUser = this.enrolmentFormStateService.enrolleeRemoteUserFormGroup();
        enrolleeRemoteUser.patchValue({
          enrolleeId,
          remoteUserId: ras.remoteUserId
        });
        this.enrolleeRemoteUsers.push(enrolleeRemoteUser);

        const remoteAccessSite = this.enrolmentFormStateService.remoteAccessSiteFormGroup();
        remoteAccessSite.patchValue({
          enrolleeId,
          siteId: ras.siteId,
          doingBusinessAs: ras.siteDoingBusinessAs,
          physicalAddress: ras.siteAddress,
          siteVendors: [{ vendorCode: ras.vendorCodes[0] }]
        });
        this.remoteAccessSites.push(remoteAccessSite);
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
    this.patchForm().subscribe(([_, enrolment]: [BcscUser, Enrolment]) => {
      // TODO refactor and make this invoke initForm
      if (enrolment.enrolleeRemoteUsers.length) {
        this.getRemoteAccess();
      }
    });
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.remoteAccessForm;
  }

  protected initForm() {
    this.form.controls.sites = this.fb.array(this.remoteAccessSearch.map(() => this.fb.control(false)));

    const checked = this.remoteAccessSearch
      .reduce((checks: boolean[], r: RemoteAccessSearch) => {
        const alreadyLinked = this.enrolleeRemoteUsers.controls
          .some(c => c.get('remoteUserId').value === r.remoteUserId);

        if (alreadyLinked) {
          checks.push(true);
        } else {
          checks.push(false);
        }

        return checks;
      }, []);

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
    this.noRemoteAccess = false;

    const certSearch: CertSearch[] = this.enrolmentFormStateService
      .regulatoryFormState
      .collegeCertifications
      .map(c => ({
        collegeCode: c.collegeCode,
        licenceNumber: c.licenseNumber
      }));

    this.siteResource.getSitesByRemoteUserInfo(certSearch)
      .pipe(delay(2000))
      .subscribe(
        (remoteAccessSearch: RemoteAccessSearch[]) => {
          if (remoteAccessSearch.length) {
            this.noRemoteAccess = false;
            this.remoteAccessSearch = remoteAccessSearch;
            this.initForm();
          } else {
            this.noRemoteAccess = true;
            this.requestAccess.checked = false;
          }
        },
        (error: any) => { }, // Noop allowing use of finally
        () => this.showProgress = false
      );
  }

  /**
   * @description
   * Remove remote access locations from the enrolment if no remote
   * sites have been chosen.
   */
  private removeRemoteAccessLocations() {
    const form = this.enrolmentFormStateService.remoteAccessLocationsForm;
    const locations = form.get('remoteAccessLocations') as FormArray;
    locations.clear();
  }
}
