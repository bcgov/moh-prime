import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';

import { Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteService } from '@registration/shared/services/site.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { EnrolleeBannerListPageComponent } from '@adjudication/pages/enrollee-banner-list-page/enrollee-banner-list-page.component';

@Component({
  selector: 'app-site-information-form',
  templateUrl: './site-information-form.component.html',
  styleUrls: ['./site-information-form.component.scss']
})
export class SiteInformationFormComponent implements OnInit {
  @Input() public form: UntypedFormGroup;
  @Input() public organizationId?: number;
  @Input() public careSettingCode: number;
  public busy: Subscription;
  public doingBusinessAsNames: string[];

  constructor(
    private organizationResource: OrganizationResource,
    private orgBookResource: OrgBookResource,
    private siteService: SiteService,
    private formUtilsService: FormUtilsService
  ) {
    this.doingBusinessAsNames = [];
  }

  public get doingBusinessAs(): UntypedFormControl {
    return this.form.get('doingBusinessAs') as UntypedFormControl;
  }

  public get pec(): UntypedFormControl {
    return this.form.get('pec') as UntypedFormControl;
  }

  public get isNewWithSiteId(): UntypedFormControl {
    return this.form.get('isNewWithSiteId') as UntypedFormControl;
  }

  public get isNewWithoutSiteId(): UntypedFormControl {
    return this.form.get('isNewWithoutSiteId') as UntypedFormControl;
  }

  public get activeBeforeRegistration(): UntypedFormControl {
    return this.form.get('activeBeforeRegistration') as UntypedFormControl;
  }

  public get deviceProviderId(): UntypedFormControl {
    return this.form.get('deviceProviderId') as UntypedFormControl;
  }

  public isCommunityPharmacy() {
    return this.siteService.site?.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY;
  }

  // TODO: Share with BusinessLicencePageComponent?
  public isDeviceProvider() {
    return this.siteService.site?.careSettingCode === CareSettingEnum.DEVICE_PROVIDER;
  }

  public ngOnInit(): void {
    this.initForm();
    this.updatePEC();
    this.updateDeviceProviderId();
  }

  protected initForm(): void {
    if (this.organizationId) {
      this.getDoingBusinessAs(this.organizationId);
    }
  }

  private getDoingBusinessAs(organizationId: number) {
    this.busy = this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        map((organization: Organization) => organization.registrationId),
        this.orgBookResource.doingBusinessAsMap(),
        tap((doingBusinessAsNames: string[]) =>
          this.doingBusinessAsNames = doingBusinessAsNames
        )
      )
      .subscribe();
  }

  public checkAsIsNewWithSiteId(change: MatCheckboxChange): void {
    if (change.checked) {
      this.activeBeforeRegistration.setValue(false);
      this.isNewWithoutSiteId.setValue(false);
    }
    this.updatePEC();
  }

  public checkAsIsNewWithoutSiteId(change: MatCheckboxChange): void {
    if (change.checked) {
      this.activeBeforeRegistration.setValue(false);
      this.isNewWithSiteId.setValue(false);
    }
    this.updatePEC();
  }

  public checkAsOperational(change: MatCheckboxChange): void {
    if (change.checked) {
      this.isNewWithoutSiteId.setValue(false);
      this.isNewWithSiteId.setValue(false);
    }
    this.updatePEC();
  }


  private updatePEC(): void {
    if ((this.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACY || this.careSettingCode === CareSettingEnum.DEVICE_PROVIDER)) {
      if (this.activeBeforeRegistration.value || this.isNewWithSiteId.value) {
        this.formUtilsService.setValidators(this.pec, [Validators.required, FormControlValidators.communityPharmacySiteId]);
        this.pec.enable();
        if (!this.pec.value || this.pec.value === "") {
          this.pec.setValue("BC00000");
        }
      } else {
        this.formUtilsService.setValidators(this.pec, []);
        this.pec.disable();
        this.pec.setValue("");
      }
    } else {
      this.formUtilsService.setValidators(this.pec, []);
    }
  }

  private updateDeviceProviderId(): void {
    if (this.careSettingCode === CareSettingEnum.DEVICE_PROVIDER) {
      this.formUtilsService.setValidators(this.deviceProviderId, [Validators.required, FormControlValidators.deviceProviderId]);
    } else {
      this.formUtilsService.setValidators(this.deviceProviderId, []);
    }
  }
}
