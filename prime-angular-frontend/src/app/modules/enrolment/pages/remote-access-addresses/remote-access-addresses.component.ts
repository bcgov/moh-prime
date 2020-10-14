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
import { Enrolment } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-remote-access-addresses',
  templateUrl: './remote-access-addresses.component.html',
  styleUrls: ['./remote-access-addresses.component.scss']
})
export class RemoteAccessAddressesComponent extends BaseEnrolmentProfilePage implements OnInit {
  public enrolment: Enrolment;
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

    this.enrolment = this.enrolmentService.enrolment;
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

  public addLocation() {
    this.addRemoteUserLocation();
  }

  public removeLocation(index: number) {
    this.remoteUserLocations.removeAt(index);

    if (!this.remoteUserLocations.controls.length) {
      this.addRemoteUserLocation();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private addRemoteUserLocation(): void {
    const remoteUserLocation = this.enrolmentFormStateService
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

  protected createFormInstance(): void {
    this.form = this.enrolmentFormStateService.remoteAccessAddressesForm;
  }

  protected initForm(): void {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.remoteUserLocations.length) {
      this.addRemoteUserLocation();
    }
  }
}
