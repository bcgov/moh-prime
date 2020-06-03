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
import { Site } from '@registration/shared/models/site.model';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state-service.service';
import { SiteService } from '@registration/shared/services/site.service';

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
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
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
        const currentRemoteUsers = this.parent.get('remoteUsers').value as RemoteUser[];
        const index = currentRemoteUsers
          .findIndex((remoteUser: RemoteUser) =>
            remoteUser.id === remoteUserId
          );
        remoteUsersFormGroup.removeAt(index);
      }

      // TODO won't work wipes out changes on route with the latest from the server!!!
      // TODO set temporary route param to prevent setting the site
      remoteUsersFormGroup.push(this.form);

      console.log('LOCAL_FORM', this.form.getRawValue());
      console.log('PARENT_FORM', remoteUsersFormGroup.getRawValue());

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
    // TODO temporary to prevent overwriting the parent form state
    this.routeUtils.routeRelativeTo(['./'], { queryParams: { fromRemoteUser: true } });
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    // Set the parent form for updating on submission, but otherwise use the
    // local form group for all changes
    this.parent = this.siteFormStateService.remoteUsersForm;
  }

  private initForm() {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // TODO don't clear the form in this component
    this.siteFormStateService.setForm(site, true);

    const remoteUserId = +this.route.snapshot.params.id;
    const remoteUser = this.parent.value.remoteUsers
      .find((r: RemoteUser) => r.id === remoteUserId);
    // Create a local form group for creating or updating remote users
    this.form = this.siteFormStateService
      .createEmptyRemoteUserFormAndPatch(remoteUser);

    // TODO ID being default zero causes so many issues
    if (remoteUserId && remoteUserId !== 0) {
      this.disableProvince(this.remoteUserLocations.controls as FormGroup[]);
    } else {
      this.addRemoteUserLocation();
    }
  }

  private addRemoteUserLocation(): void {
    const remoteUserLocation = this.siteFormStateService
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
