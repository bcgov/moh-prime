import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray } from '@angular/forms';

import { Subscription } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { RemoteUserLocation } from '@registration/shared/models/remote-user-location.model';
import { Province } from '@shared/enums/province.enum';
import { Country } from '@shared/enums/country.enum';

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
    // TODO update the global form with changes only on submission
    if (this.formUtilsService.checkValidity(this.form)) {
      const remoteUserId = +this.route.snapshot.params.id;
    }
  }

  public onAdd() {
    const remoteUserLocation = this.siteRegistrationStateService
      .remoteUserLocationFormGroup();
    const physicalAddress = remoteUserLocation.get('physicalAddress');
    physicalAddress.patchValue({
      countryCode: Country.CANADA,
      provinceCode: Province.BRITISH_COLUMBIA
    });
    physicalAddress.get('provinceCode').disable();
    this.remoteUserLocations.push(remoteUserLocation);
  }

  public onRemove(index: number) {
    this.remoteUserLocations.removeAt(index);

    if (!this.remoteUserLocations.controls.length) {
      this.onAdd();
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
    // TODO setup in site state service and maintain list
    // TODO view the updates from the state service in remote users
    // TODO DO NOT update state service form directly when editing... use a copy
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

    // Indicates whether this is a create (id=0) or update (id>0)
    const remoteUserId = +this.route.snapshot.params.id;
    const remoteUser = this.parent.value.remoteUsers
      .find((r: RemoteUser) => r.id === remoteUserId);
    // Create a local form group for creating or updating remote users
    this.form = this.siteRegistrationStateService
      .createEmptyRemoteUserFormAndPatch(remoteUser);

    this.remoteUserLocations.controls
      .forEach((remoteUserLocation: FormGroup) =>
        remoteUserLocation.get('physicalAddress.provinceCode').disable()
      );
  }
}
