import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { noop, of } from 'rxjs';

import { CollegeConfig, LicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { AddressLine } from '@lib/models/address.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { RemoteUser } from '@lib/models/remote-user.model';
import { RemoteUserCertification } from '@lib/models/remote-user-certification.model';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { RemoteUsersPageFormState } from '../remote-users-page/remote-users-page-form-state.class';

@Component({
  selector: 'app-remote-user-page',
  templateUrl: './remote-user-page.component.html',
  styleUrls: ['./remote-user-page.component.scss']
})
export class RemoteUserPageComponent extends AbstractEnrolmentPage implements OnInit {
  /**
   * @description
   * FormState of the parent form, which has reuse in child form
   * with regards to helper methods.
   */
  public formState: RemoteUsersPageFormState;
  /**
   * @description
   * Local form for adding and updating a single model that is
   * not linked with the form state until submission where it
   * gets mirrored.
   */
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  /**
   * @description
   * URL parameter indicating the ID of the remote user, or
   * `new` when the user does not already exist.
   */
  public remoteUserIndex: string;
  public remoteUser: RemoteUser;
  public licenses: LicenseConfig[];
  public formControlNames: AddressLine[];
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteService: SiteService,
    private siteFormStateService: SiteFormStateService,
    private configService: ConfigService,
    private enrolmentService: EnrolmentService,
    route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.licenses = this.configService.licenses;
    this.remoteUserIndex = route.snapshot.params.index;
  }

  /**
   * @description
   * Remote user certifications specific to the local form.
   */
  public get remoteUserCertification(): FormGroup {
    return this.form.get('remoteUserCertification') as FormGroup;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['./'], { queryParams: { isFormDirty: this.form.dirty } });
  }

  public collegeFilterPredicate() {
    return (collegeConfig: CollegeConfig) =>
      (collegeConfig.code === CollegeLicenceClassEnum.CPSBC || collegeConfig.code === CollegeLicenceClassEnum.BCCNM);
  }

  public licenceFilterPredicate() {
    return (licenceConfig: LicenseConfig) =>
      this.enrolmentService.hasAllowedRemoteAccessLicences(licenceConfig);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    // Be aware that this is the parent form state and should only
    // be used for it's API and on submission
    this.formState = this.siteFormStateService.remoteUsersPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;

    // Attempt to patch if needed on a refresh, otherwise do not forcibly
    // update the form state as it will drop unsaved updates
    this.siteFormStateService.setForm(site, false);

    // Extract an existing remoteUser from the parent form for updates, otherwise new
    const remoteUser = this.formState.getRemoteUsers()[+this.remoteUserIndex] ?? null;

    // Remote user at index does not exist likely due to a browser
    // refresh on this page, and the URL param should be update
    if (this.remoteUserIndex !== 'new' && !remoteUser) {
      this.routeUtils.routeRelativeTo(['new']);
    }

    // Create a local form group for creating or updating remote users
    this.form = this.formState.createEmptyRemoteUserFormAndPatch(remoteUser);
  }

  protected checkValidity(): boolean {
    // Pass the local form for validation and submission instead
    // of using the default form from the form state
    return super.checkValidity(this.form);
  }

  protected performSubmission(): NoContent {
    // Set the parent form for updating on submission, but otherwise use the
    // local form group for all changes prior to submission
    const parent = this.formState.form;
    const remoteUsersFormArray = parent.get('remoteUsers') as FormArray;

    if (this.remoteUserIndex !== 'new') {
      const remoteUserFormGroup = remoteUsersFormArray.at(+this.remoteUserIndex);

      // Replace the updated remote user in the parent form for submission
      remoteUserFormGroup.reset(this.form.getRawValue());
    } else {
      // Store the new remote user in the parent form for submission
      remoteUsersFormArray.push(this.form);
    }

    return of(noop());
  }

  protected afterSubmitIsSuccessful(): void {
    // Inform the remote users view not to patch the form, otherwise updates will be lost
    this.routeUtils.routeRelativeTo(['./'], { queryParams: { fromRemoteUser: true, isFormDirty: this.form.dirty } });
  }
}
