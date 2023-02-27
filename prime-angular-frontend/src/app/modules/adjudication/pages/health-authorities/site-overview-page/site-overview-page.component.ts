import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { Component, OnInit } from '@angular/core';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { asyncValidator } from '@lib/validators/form-async.validators';

import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { HealthAuthoritySiteAdmin } from '@health-auth/shared/models/health-authority-admin-site.model';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { MatDialog } from '@angular/material/dialog';

interface HealthAuthorityVendorMap extends VendorConfig {
  id?: number;
}
@Component({
  selector: 'app-site-overview-page',
  templateUrl: './site-overview-page.component.html',
  styleUrls: ['./site-overview-page.component.scss']
})
export class SiteOverviewPageComponent implements OnInit {
  public busy: Subscription;
  public site: HealthAuthoritySiteAdmin;
  public healthAuthoritySite: HealthAuthoritySite;
  public form: FormGroup;
  public vendorForm: FormGroup;
  public healthAuthorityVendors: HealthAuthorityVendorMap[];
  public refresh: BehaviorSubject<boolean>;
  public vendor: VendorConfig;

  constructor(
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    private formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private configService: ConfigService,
    private siteResource: SiteResource,
    private dialog: MatDialog,
  ) {
    this.refresh = new BehaviorSubject<boolean>(null);
    this.healthAuthorityVendors = this.configService.vendors
      .filter((vendorConfig: VendorConfig) => vendorConfig.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) as HealthAuthorityVendorMap[];

  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public get vendors(): FormArray {
    return this.vendorForm.get('vendors') as FormArray;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = +this.route.snapshot.params.sid;
      const pec = this.form.value.pec;
      this.busy = this.siteResource.updatePecCode(siteId, pec)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.pec = pec;
        });
    }
  }

  public onSubmitVendor(): void {

    if (this.formUtilsService.checkValidity(this.vendorForm)) {
      const siteId = +this.route.snapshot.params.sid;
      const vendor = this.vendor;

      const data: DialogOptions = {
        title: 'Change Vendor',
        message: "Are you sure you want to change this site's vendor?",
        actionType: 'warn',
        actionText: 'Change Vendor',
        component: NoteComponent,
      };

      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe(rationale => {
        console.log(rationale.output);

        this.siteResource.updateVendor(siteId, vendor.code)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.healthAuthorityVendor.vendorCode = vendor.code;
        });
      });
    }  
  }

  public onVendorChange(value: VendorConfig) {
    this.vendor = value;
  }


  public ngOnInit(): void {
    this.createFormInstance();
    this.busy = this.healthAuthorityResource
      .getHealthAuthorityAdminSite(+this.route.snapshot.params.haid, +this.route.snapshot.params.sid)
      .subscribe((site: HealthAuthoritySiteAdmin) => {
        this.site = site;
        this.initForm(site);
      });
    
    this.vendors.markAsTouched();
    this.pec.markAsTouched();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ]
    });

    this.vendorForm = this.fb.group({
      vendors: this.fb.array([])
    });
  }

  private initForm({ pec, healthAuthorityVendor }: HealthAuthoritySiteAdmin): void {
    this.form.patchValue({ pec });
    this.vendorForm.patchValue({ healthAuthorityVendor });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.route.snapshot.params.sid, value);
  }
}
