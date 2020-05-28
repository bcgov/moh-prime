import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray } from '@angular/forms';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';
import { Country } from '@shared/enums/country.enum';
import { Province } from '@shared/enums/province.enum';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

@Component({
  selector: 'app-remote-user',
  templateUrl: './remote-user.component.html',
  styleUrls: ['./remote-user.component.scss']
})
export class RemoteUserComponent implements OnInit {
  public busy: Subscription;
  public parent: FormGroup;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public remoteUser: RemoteUser;
  public formControlNames: string[];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private formUtilsService: FormUtilsService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public get remoteUserLocations(): FormArray {
    return this.form.get('remoteUserLocations') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const remoteUserId = +this.route.snapshot.params.id;
      const remoteUsersFormGroup = this.parent.get('remoteUsers') as FormArray;

      if (remoteUserId) {
        // Update the existing remote user
        const currentRemoteUsers = this.parent.get('remoteUsers').value as RemoteUser[];
        const index = currentRemoteUsers
          .findIndex((remoteUser: RemoteUser) =>
            remoteUsersFormGroup.value.id === remoteUserId
          );
        remoteUsersFormGroup.removeAt(index);
      }

      console.log('LOCAL_FORM', this.form.getRawValue());
      console.log('PARENT_FORM', remoteUsersFormGroup.getRawValue());

      remoteUsersFormGroup.push(this.form);
      this.nextRoute();
    }
  }

  public onAdd() {
    this.addRemoteUserLocation();
  }

  public onRemove(index: number) {
    this.remoteUserLocations.removeAt(index);

    if (!this.remoteUserLocations.controls.length) {
      this.addRemoteUserLocation();
    }
  }

  public onBack() {
    // TODO canDeactive check if unsaved changes
    this.routeUtils.routeRelativeTo(['./']);
  }

  public nextRoute() {
    this.routeUtils.routeRelativeTo(['./']);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    // Set the parent form for updating on submission, but otherwise use the
    // local form group for all changes
    this.parent = this.siteRegistrationStateService.remoteUsersForm;
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site?.completed;
    this.siteRegistrationStateService.setSite(site, true);

    const remoteUserId = +this.route.snapshot.params.id;
    const remoteUser = this.parent.value.remoteUsers
      .find((r: RemoteUser) => r.id === remoteUserId);
    // Create a local form group for creating or updating remote users
    this.form = this.siteRegistrationStateService
      .createEmptyRemoteUserFormAndPatch(remoteUser);

    if (remoteUserId) {
      this.disableProvince(this.remoteUserLocations.controls as FormGroup[]);
    } else {
      this.addRemoteUserLocation();
    }
  }

  private addRemoteUserLocation(): void {
    const remoteUserLocation = this.siteRegistrationStateService
      .remoteUserLocationFormGroup();
    remoteUserLocation.get('physicalAddress')
      .patchValue({
        countryCode: Country.CANADA,
        provinceCode: Province.BRITISH_COLUMBIA
      });
    this.disableProvince(remoteUserLocation);

    this.remoteUserLocations.push(remoteUserLocation);
  }

  private disableProvince(remoteUserLocationFormGroups: FormGroup | FormGroup[]): void {
    (Array.isArray(remoteUserLocationFormGroups))
      ? remoteUserLocationFormGroups.forEach(group => this.disableProvince(group))
      : remoteUserLocationFormGroups.get('physicalAddress.provinceCode').disable();
  }
}
