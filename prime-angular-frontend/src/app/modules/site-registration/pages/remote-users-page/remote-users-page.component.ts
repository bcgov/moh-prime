import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { noop, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { RemoteUser } from '@lib/models/remote-user.model';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { AbstractSiteRegistrationPage } from '@registration/shared/classes/abstract-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { RemoteUsersPageFormState } from './remote-users-page-form-state.class';

@UntilDestroy()
@Component({
  selector: 'app-remote-users-page',
  templateUrl: './remote-users-page.component.html',
  styleUrls: ['./remote-users-page.component.scss']
})
export class RemoteUsersPageComponent extends AbstractSiteRegistrationPage implements OnInit {
  public formState: RemoteUsersPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public hasNoRemoteUserError: boolean;
  public hasNoEmailError: boolean;
  public submitButtonText: string;
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

    this.canDeactivateAllowlist = ['hasRemoteUsers'];

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.submitButtonText = 'Save and Continue';
  }

  public getRemoteUserProperties(remoteUser: FormGroup) {
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

  public onRemove(index: number) {
    this.formState.remoteUsers.removeAt(index);
  }

  public onEdit(index: number) {
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.REMOTE_USERS, index]);
  }

  public onBack() {
    const nextRoute = (!this.isCompleted)
      ? SiteRoutes.HOURS_OPERATION
      : SiteRoutes.SITE_REVIEW;

    this.routeUtils.routeRelativeTo(['../', nextRoute]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();

    if (this.hasBeenSubmitted) {
      this.submitButtonText = 'Save and Submit';
    }
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
    this.formState.form.markAsPristine();
  }

  protected initForm() {
    this.formState.remoteUsers.valueChanges
      .pipe(untilDestroyed(this))
      .subscribe((remoteUsers: RemoteUser[]) => {
        (remoteUsers.length)
          ? this.formState.hasRemoteUsers.disable({ emitEvent: false })
          : this.formState.hasRemoteUsers.enable({ emitEvent: false });
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

    this.patchForm();
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
    const newRemoteUsers = this.siteFormStateService.remoteUsersPageFormState.json
      .reduce((newRemoteUsersAcc: RemoteUser[], updated: RemoteUser) => {
        if (!site.remoteUsers.find((current: RemoteUser) =>
          current.firstName === updated.firstName &&
          current.lastName === updated.lastName &&
          current.email === updated.email
        )) {
          newRemoteUsersAcc.push(updated);
        }
        return newRemoteUsersAcc;
      }, []);

    return this.siteResource.updateSite(payload)
      .pipe(
        exhaustMap(() =>
          (site.submittedDate)
            ? this.siteResource.sendRemoteUsersEmailAdmin(site.id)
            : of(noop())
        ),
        exhaustMap(() =>
          (site.submittedDate && newRemoteUsers)
            ? this.siteResource.sendRemoteUsersEmailUser(site.id, newRemoteUsers)
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
}
