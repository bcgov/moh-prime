import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormGroup, UntypedFormArray, UntypedFormControl, AbstractControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { noop, Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { CollegeConfig, LicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { AddressLine } from '@lib/models/address.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { RemoteUser } from '@lib/models/remote-user.model';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { RemoteUsersPageFormState } from '../remote-users-page/remote-users-page-form-state.class';
import { Site } from '@registration/shared/models/site.model';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

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
  public form: UntypedFormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public isSubmitted: boolean;
  public hasDuplicateError: boolean;
  /**
   * @description
   * URL parameter indicating the ID of the remote user, or
   * `new` when the user does not already exist.
   */
  public remoteUserIndex: string;
  public remoteUser: RemoteUser;
  public site: Site;
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
    this.site = siteService.site;
  }

  /**
   * @description
   * Remote user certifications specific to the local form.
   */
  public get remoteUserCertification(): UntypedFormGroup {
    return this.form.get('remoteUserCertification') as UntypedFormGroup;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['./'], { queryParams: { fromRemoteUser: true } });
  }

  public collegeFilterPredicate() {
    if (this.site.remoteAccessTypeCode) {
      var remoteUserLicense = this.licenses.filter(l => l.remoteAccessTypeLicenses && l.remoteAccessTypeLicenses.some(r => r.remoteAccessTypeCode === this.site.remoteAccessTypeCode));
      return (collegeConfig: CollegeConfig) =>
        collegeConfig.collegeLicenses.some(l => remoteUserLicense.some(rul => rul.collegeLicenses.some(r => r.licenseCode === l.licenseCode)));
    } else {
      return false;
    }
  }

  public licenceFilterPredicate() {
    if (this.site.remoteAccessTypeCode) {
      return (licenceConfig: LicenseConfig) =>
        licenceConfig.remoteAccessTypeLicenses.length &&
        licenceConfig.remoteAccessTypeLicenses.some((r) => r.remoteAccessTypeCode === this.site.remoteAccessTypeCode);
    } else {
      return false;
    }
  }

  /**
   * @description
   * Deactivation guard handler. Reworked to use form, !formState
   */
  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty && !this.allowRoutingWhenDirty)
      ? this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          map((confirmation: boolean) => {
            this.handleDeactivation(confirmation);
            return confirmation;
          })
        )
      : true;
  }

  /**
   * @description
   * Form submission event handler. Reworked to use form, !formState
   */
  public onSubmit(): void {
    this.hasAttemptedSubmission = true;

    if (this.checkValidity(this.form) && !this.checkDuplicate()) {
      this.onSubmitFormIsValid();
      this.busy = this.performSubmission()
        .pipe(tap((_) => this.form.markAsPristine()))
        .subscribe(() => this.afterSubmitIsSuccessful());
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  protected onSubmitFormIsInvalid() {
    this.hasDuplicateError = true;
  }

  protected onSubmitFormIsValid() {
    this.hasDuplicateError = false;
  }

  protected createFormInstance() {
    // Be aware that this is the parent form state and should only
    // be used for it's API and on submission
    this.formState = this.siteFormStateService.remoteUsersPageFormState;
  }

  protected initForm(): void {
    this.patchForm();
  }

  protected checkDuplicate(): boolean {
    const remoteUser = this.form.getRawValue();
    var remoteUserList = this.formState.form.get('remoteUsers').getRawValue();

    if (this.remoteUserIndex !== "new") {
      remoteUserList.splice(this.remoteUserIndex, 1);
    }

    return remoteUserList.filter(s =>
      s.remoteUserCertification.collegeCode === remoteUser.remoteUserCertification.collegeCode &&
      s.remoteUserCertification.licenseCode === remoteUser.remoteUserCertification.licenseCode &&
      s.remoteUserCertification.licenseNumber === remoteUser.remoteUserCertification.licenseNumber &&
      s.remoteUserCertification.practitionerId === remoteUser.remoteUserCertification.practitionerId
    ).length > 0;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.isSubmitted = site?.submittedDate ? true : false;

    // Attempt to patch if needed on a refresh, otherwise do not forcibly
    // update the form state as it will drop unsaved updates
    this.siteFormStateService.setForm(site, false);

    // Extract an existing remoteUser from the parent form for updates, otherwise new
    const remoteUser = this.formState.getRemoteUsers()[+this.remoteUserIndex] ?? null;

    // Remote user at index does not exist likely due to a browser
    // refresh on this page, and the URL param should be updated
    if (this.remoteUserIndex !== 'new' && !remoteUser) {
      this.routeUtils.routeRelativeTo(['new']);
    }

    // Create a local form group for creating or updating remote users
    this.form = this.formState.createEmptyRemoteUserFormAndPatch(remoteUser);
  }

  protected performSubmission(): NoContent {
    // Set the parent form for updating on submission, but otherwise use the
    // local form group for all changes prior to submission
    const parent = this.formState.form;
    const remoteUsersFormArray = parent.get('remoteUsers') as UntypedFormArray;

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
    this.formState.form.markAsDirty();
    // Inform the remote users view not to patch the form, otherwise updates will be lost
    this.routeUtils.routeRelativeTo(['./'], { queryParams: { fromRemoteUser: true } });
  }
}
