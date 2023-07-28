import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
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
  @Input() public form: FormGroup;
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

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public get isNew(): FormControl {
    return this.form.get('isNew') as FormControl;
  }

  public get activeBeforeRegistration(): FormControl {
    return this.form.get('activeBeforeRegistration') as FormControl;
  }

  public isCommunityPharmacy() {
    return this.siteService.site?.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST;
  }

  public ngOnInit(): void {
    this.initForm();
    this.updatePECValidator();
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

  public checkAsIsNew(change: MatCheckboxChange): void {
    if (change.checked) {
      this.activeBeforeRegistration.setValue(false);
      this.pec.setValue("");
    }
    this.updatePECValidator();
  }

  public checkAsOperational(change: MatCheckboxChange): void {
    if (change.checked) {
      this.isNew.setValue(false);
    }
    this.updatePECValidator();
  }

  private updatePECValidator(): void {
    if (this.careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST) {
      if (this.activeBeforeRegistration.value) {
        this.formUtilsService.setValidators(this.pec, [Validators.required, FormControlValidators.communityPharmacySiteId]);
      } else {
        this.formUtilsService.setValidators(this.pec, [FormControlValidators.communityPharmacySiteId]);
      }
    } else {
      this.formUtilsService.setValidators(this.pec, []);
    }
  }
}
