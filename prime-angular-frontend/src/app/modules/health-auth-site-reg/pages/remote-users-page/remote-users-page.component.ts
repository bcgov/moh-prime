import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { KeyValue } from '@angular/common';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { noop, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { RemoteUser } from '@lib/models/remote-user.model';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthorityFormStateService } from '@health-auth/shared/services/health-authority-form-state.service';
import { AbstractHealthAuthoritySiteRegistrationPage } from '@health-auth/shared/classes/abstract-health-authority-site-registration-page.class';
import { RemoteUsersFormState } from './remote-users-form-state.class';

@UntilDestroy()
@Component({
  selector: 'app-remote-users-page',
  templateUrl: './remote-users-page.component.html',
  styleUrls: ['./remote-users-page.component.scss']
})
export class RemoteUsersPageComponent extends AbstractHealthAuthoritySiteRegistrationPage implements OnInit {
  public formState: RemoteUsersFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasNoRemoteUserError: boolean;
  public hasNoEmailError: boolean;
  public SiteRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected route: ActivatedRoute,
    protected siteService: HealthAuthoritySiteService,
    protected formStateService: HealthAuthorityFormStateService,
    protected healthAuthorityResource: HealthAuthorityResource,
    router: Router
  ) {
    super(dialog, formUtilsService, route, siteService, formStateService, healthAuthorityResource);

    this.canDeactivateAllowlist = ['hasRemoteUsers'];

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public getRemoteUserProperties(remoteUser: FormGroup): KeyValue<string, string>[] {
    const remoteUserCertifications = remoteUser.controls?.remoteUserCertifications as FormArray;

    const collegeLicence = (remoteUserCertifications.length > 1)
      ? 'More than one College licence'
      : (remoteUserCertifications.length === 0)
        ? 'No College licence'
        : remoteUserCertifications.value[0].licenseNumber;

    return [
      {
        key: 'College Licence',
        value: collegeLicence
      }
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
    this.formState = this.formStateService.remoteUserFormState;
  }

  protected patchForm(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      throw new Error('No health authority site ID was provided');
    }

    const site = this.siteService.site;
    this.isCompleted = site?.completed;

    // Inform the parent not to patch the form as there are outstanding changes
    // to the remote users that need to be persisted
    const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';

    // Remove query param from URL without refreshing
    this.routeUtils.removeQueryParams({ fromRemoteUser: null });
    this.formStateService.setForm(site, !this.hasBeenSubmitted && !fromRemoteUser);
    // TODO is this needed?
    this.formState.form.markAsPristine();
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

  protected onSubmitFormIsValid(): void {
    this.hasNoRemoteUserError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoRemoteUserError = true;
  }

  protected submissionRequest(): NoContent {
    // const payload = this.siteFormStateService.json;
    // const site = this.siteService.site;
    // const newRemoteUsers = this.siteFormStateService.remoteUsersPageFormState.json
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
    //
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
    const nextRoutePath = (!this.isCompleted)
      ? HealthAuthSiteRegRoutes.ADMINISTRATOR
      : HealthAuthSiteRegRoutes.SITE_OVERVIEW;

    this.routeUtils.routeRelativeTo(['../', nextRoutePath]);
  }
}
