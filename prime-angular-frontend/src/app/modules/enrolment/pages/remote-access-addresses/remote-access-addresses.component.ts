import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { Country } from '@shared/enums/country.enum';
import { Province } from '@shared/enums/province.enum';

@Component({
  selector: 'app-remote-access-addresses',
  templateUrl: './remote-access-addresses.component.html',
  styleUrls: ['./remote-access-addresses.component.scss']
})
export class RemoteAccessAddressesComponent extends BaseEnrolmentProfilePage implements OnInit {
  // public enrolment: Enrolment;
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
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder
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
      formUtilsService
    );

    // this.enrolment = this.enrolmentService.enrolment;
    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public get remoteAccessLocations(): FormArray {
    return this.form.get('remoteAccessLocations') as FormArray;
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

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  private addRemoteAccessLocation(): void {
    const remoteAccessLocation = this.enrolmentFormStateService
      .remoteAccessLocationFormGroup();
    remoteAccessLocation.get('physicalAddress')
      .patchValue({
        countryCode: Country.CANADA,
        provinceCode: Province.BRITISH_COLUMBIA
      });
    this.disableProvince(remoteAccessLocation);

    this.remoteAccessLocations.push(remoteAccessLocation);
  }

  private disableProvince(remoteAccessLocationFormGroups: FormGroup | FormGroup[]): void {
    (Array.isArray(remoteAccessLocationFormGroups))
      ? remoteAccessLocationFormGroups.forEach(group => this.disableProvince(group))
      : remoteAccessLocationFormGroups.get('physicalAddress.provinceCode').disable();
  }

  protected createFormInstance(): void {
    this.form = this.enrolmentFormStateService.remoteAccessLocationsForm;
  }

  protected initForm(): void {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.remoteAccessLocations.length) {
      this.addRemoteAccessLocation();
    }
  }

  protected nextRouteAfterSubmit() {
    super.nextRouteAfterSubmit(this.EnrolmentRoutes.CARE_SETTING);
  }
}
