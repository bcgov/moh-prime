import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { KeyValue } from '@angular/common';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
// TODO move to @lib/models
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';


import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { RemoteUsersPageFormState } from './remote-users-form-state.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';

// TODO refactor into list/form composite component used in health authority organization information
// TODO copy of the remote users and remote user have been pulled from site registration
//      and do not fit the current workflow for health authorities. Remote users should
//      be set up similar to adjudication/pages/health-authorities/vendor-page where
//      the list and form exist in one page and allow for a single form state to be
//      shared since form state service is not used
@UntilDestroy()
@Component({
  selector: 'app-remote-users-page',
  templateUrl: './remote-users-page.component.html',
  styleUrls: ['./remote-users-page.component.scss']
})
export class RemoteUsersPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: RemoteUsersPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasNoRemoteUserError: boolean;
  public hasNoEmailError: boolean;
  public submitButtonText: string;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.canDeactivateAllowlist = ['hasRemoteUsers'];

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
    this.submitButtonText = 'Save and Continue';
  }

  public getRemoteUserProperties(remoteUser: FormGroup): KeyValue<string, string>[] {
    const remoteUserCertifications = remoteUser.controls?.remoteUserCertifications as FormArray;

    const collegeLicence = (remoteUserCertifications.length > 1)
      ? 'More than one College licence'
      : (remoteUserCertifications.length === 0)
        ? 'No College licence'
        : remoteUserCertifications.value[0].licenseNumber;

    return [
      { key: 'College Licence', value: collegeLicence }
    ];
  }

  public onRemove(index: number): void {
    this.formState.remoteUsers.removeAt(index);
  }

  public onEdit(index: number): void {
    this.routeUtils.routeRelativeTo([HealthAuthSiteRegRoutes.REMOTE_USERS, index]);
  }

  public onBack(): void {
    const backRoutePath = (this.isCompleted)
      ? HealthAuthSiteRegRoutes.SITE_OVERVIEW
      : HealthAuthSiteRegRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(['../', backRoutePath]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance(): void {
    this.formState = new RemoteUsersPageFormState(this.fb);
  }

  protected initForm(): void {
    this.formState.remoteUsers.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((remoteUsers: RemoteUser[]) =>
        (remoteUsers.length)
          ? this.formState.hasRemoteUsers.disable({ emitEvent: false })
          : this.formState.hasRemoteUsers.enable({ emitEvent: false })
      );

    this.formState.hasRemoteUsers.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((hasRemoteUsers: boolean) => {
        (hasRemoteUsers)
          ? this.formState.remoteUsers.setValidators(FormArrayValidators.atLeast(1))
          : this.formState.remoteUsers.clearValidators();

        this.hasNoRemoteUserError = false;
        this.formState.remoteUsers.updateValueAndValidity({ emitEvent: false });
      });

    this.patchForm();
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      return;
    }

    this.busy = this.healthAuthorityResource.getHealthAuthoritySiteById(healthAuthId, healthAuthSiteId)
      .subscribe(({ remoteUsers, completed, submittedDate }: HealthAuthoritySite) => {
        this.isCompleted = completed;
        // Inform the parent not to patch the form as there are outstanding changes
        // to the remote users that need to be persisted
        const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';
        // Remove query param from URL without refreshing
        this.routeUtils.removeQueryParams({ fromRemoteUser: null });
        this.formState.patchValue({ remoteUsers });

        if (submittedDate) {
          this.submitButtonText = 'Save and Submit';
        }
      });
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoRemoteUserError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoRemoteUserError = true;
  }

  protected performSubmission(): NoContent {
    const payload = this.formState.json;
    const { haid, sid } = this.route.snapshot.params;

    return this.healthAuthorityResource.updateHealthAuthoritySiteRemoteUsers(haid, sid, payload);

    // TODO do we need to send emails to remote users?
    // const newRemoteUsers = this.formStateService.remoteUsersPageFormState.json
    //   .reduce((newRemoteUsersAcc: RemoteUser[], updated: RemoteUser) => {
    //     if (!site.remoteUsers.find((current: RemoteUser) =>
    //       current.firstName === updated.firstName &&
    //       current.lastName === updated.lastName &&
    //       current.email === updated.email
    //     )) {
    //       newRemoteUsersAcc.push(updated);
    //     }
    //     return newRemoteUsersAcc;
    //   }, []);

    // return this.siteResource.updateSite(payload)
    //   .pipe(
    //     exhaustMap(() =>
    //       (site.submittedDate)
    //         ? this.siteResource.sendRemoteUsersEmailAdmin(site.id)
    //         : of(noop())
    //     ),
    //     exhaustMap(() =>
    //       (site.submittedDate && newRemoteUsers)
    //         ? this.siteResource.sendRemoteUsersEmailUser(site.id, newRemoteUsers)
    //         : of(noop())
    //     )
    //   );
  }

  protected afterSubmitIsSuccessful(): void {
    // TODO should account for updates which would redirect back to SiteManagement
    const nextRoutePath = (!this.isCompleted)
      ? HealthAuthSiteRegRoutes.ADMINISTRATOR
      : HealthAuthSiteRegRoutes.SITE_OVERVIEW;

    this.routeUtils.routeRelativeTo(['../', nextRoutePath]);
  }
}
