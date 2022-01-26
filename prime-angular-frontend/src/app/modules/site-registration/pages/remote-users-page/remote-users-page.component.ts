import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { KeyValue } from '@angular/common';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { noop, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { RemoteUsersPageFormState } from './remote-users-page-form-state.class';

@UntilDestroy()
@Component({
  selector: 'app-remote-users-page',
  templateUrl: './remote-users-page.component.html',
  styleUrls: ['./remote-users-page.component.scss']
})
export class RemoteUsersPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: RemoteUsersPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasNoRemoteUserError: boolean;
  public hasNoEmailError: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService, siteService, siteFormStateService, siteResource);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public getRemoteUserProperties(remoteUser: FormGroup): KeyValue<string, string>[] {
    const remoteUserCertification = remoteUser.controls?.remoteUserCertification as FormGroup;
    return [
      {
        key: 'College Licence',
        value: remoteUserCertification.value.licenseNumber
      }
    ];
  }

  public onRemove(index: number) {
    this.formState.remoteUsers.removeAt(index);

    // After removing a remote user, always mark form as dirty
    this.formState.form.markAsDirty();
  }

  public onEdit(index: number) {
    this.allowRoutingWhenDirty = true;
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.REMOTE_USERS, index]);
  }

  public onAdd() {
    this.allowRoutingWhenDirty = true;
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.REMOTE_USERS, 'new']);
  }

  public onBack() {
    const nextRoute = (!this.isCompleted)
      ? SiteRoutes.HOURS_OPERATION
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(['../', nextRoute]);
  }

  public onToggleChange() {
    this.formState.hasRemoteUsers.markAsPristine();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.remoteUsersPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;

    // Inform the parent not to patch the form as there are outstanding changes
    // to the remote users that need to be persisted
    const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';

    // Remove query param from URL without refreshing
    this.routeUtils.removeQueryParams({ fromRemoteUser: null });
    this.siteFormStateService.setForm(site, !this.hasBeenSubmitted && !fromRemoteUser);

    // Needed if returning from Add/Update Remote User
    this.setHasRemoteUsersToggleState();

    if (!fromRemoteUser) {
      this.formState.form.markAsPristine();
    }
  }

  protected initForm() {
    this.formState.remoteUsers.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((_) => {
        // Executed when removing Remote Users
        this.setHasRemoteUsersToggleState();
      });

    this.formState.hasRemoteUsers.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((hasRemoteUsers: boolean) => {
        (hasRemoteUsers)
          ? this.formState.remoteUsers.setValidators(FormArrayValidators.atLeast(1))
          : this.formState.remoteUsers.clearValidators();

        this.hasNoRemoteUserError = false;
        this.formState.remoteUsers.updateValueAndValidity({ emitEvent: false });
      });

    this.formState.remoteUsers.length
      ? this.formState.hasRemoteUsers.setValue(true)
      : this.formState.hasRemoteUsers.setValue(false)

    this.patchForm();
  }

  private setHasRemoteUsersToggleState(): void {
    this.formState.remoteUsers.length
      ? this.formState.hasRemoteUsers.disable({ emitEvent: false })
      : this.formState.hasRemoteUsers.enable({ emitEvent: false });
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoRemoteUserError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoRemoteUserError = true;
  }

  protected submissionRequest(): NoContent {
    const payload = this.siteFormStateService.json;
    const site = this.siteService.site;

    return this.siteResource.updateSite(payload)
      .pipe(
        exhaustMap(() =>
          (site.submittedDate)
            ? this.siteResource.sendRemoteUsersEmailAdmin(site.id)
            : of(noop())
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (!this.isCompleted)
      ? SiteRoutes.ADMINISTRATOR
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(['../', routePath]);
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Reset the remoteUsersPage form value
    const site = this.siteService.site;
    this.siteFormStateService.remoteUsersPageFormState.patchValue(site?.remoteUsers, true);
  }
}
