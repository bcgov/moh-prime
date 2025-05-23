import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, UntypedFormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';

import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';

import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-remote-access-addresses',
  templateUrl: './remote-access-addresses.component.html',
  styleUrls: ['./remote-access-addresses.component.scss']
})
export class RemoteAccessAddressesComponent extends BaseEnrolmentProfilePage implements OnInit {
  public formControlNames: string[];

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
    protected authService: AuthService
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

    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public get remoteAccessLocations(): UntypedFormArray {
    return this.form.get('remoteAccessLocations') as UntypedFormArray;
  }

  public addLocation() {
    this.addRemoteAccessLocation();
  }

  public removeLocation(index: number) {
    this.remoteAccessLocations.removeAt(index);

    if (!this.remoteAccessLocations.controls.length) {
      this.addRemoteAccessLocation();
    }
  }

  public onBack(route: string = '') {
    this.removeIncompleteLocations();
    super.onBack(route);
  }

  /**
   * @description
   * Removes incomplete locations from the list in preparation
   * for submission
   */
  private removeIncompleteLocations() {
    this.remoteAccessLocations.controls
      .forEach((control: UntypedFormGroup, index: number) => {
        if (!control.get('internetProvider').value || control.invalid) {
          this.remoteAccessLocations.removeAt(index);
        }
      });
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.form = this.enrolmentFormStateService.remoteAccessLocationsForm;
  }

  protected initForm(): void {
    // Always have at least one location ready for
    // the enrollee to fill out
    if (!this.remoteAccessLocations.length) {
      this.addRemoteAccessLocation();
    }
  }

  protected handleDeactivation(result: boolean): void {
    if (!result) {
      return;
    }

    // Replace previous values on deactivation so updates are discarded
    const { remoteAccessLocations } = this.enrolmentService.enrolment;
    this.enrolmentFormStateService.patchRemoteAccessLocationsForm(remoteAccessLocations);
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = this.EnrolmentRoutes.SELF_DECLARATION;
    }
    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private addRemoteAccessLocation(): void {
    const remoteAccessLocation = this.enrolmentFormStateService
      .remoteAccessLocationFormGroup();
    this.remoteAccessLocations.push(remoteAccessLocation);
  }
}
