import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BehaviorSubject, mergeMap, Observable, Subscription } from 'rxjs';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { asyncValidator } from '@lib/validators/form-async.validators';

import { HealthAuthoritySiteAdmin } from '@health-auth/shared/models/health-authority-admin-site.model';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ChangeVendorNoteComponent } from '@shared/components/dialogs/content/change-vendor-note/change-vendor-note.component';
import { MatDialog } from '@angular/material/dialog';
import { HealthAuthority } from '@shared/models/health-authority.model';

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
  public healthAuthorityVendors: HealthAuthorityVendorMap[];
  public refresh: BehaviorSubject<boolean>;

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
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public get vendors(): FormControl {
    return this.form.get('vendors') as FormControl;
  }

  public saveSiteId(): void {
    if (this.pec.valid) {
      const siteId = +this.route.snapshot.params.sid;
      const pec = this.form.value.pec;
      this.busy = this.siteResource.updatePecCode(siteId, pec)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.pec = pec;
        });
    }
  }

  public saveVendor(): void {
    const vendor = this.vendors.value;
    const existingVendor = this.healthAuthorityVendors.find((vendor: VendorConfig) => vendor.code === this.site.healthAuthorityVendor.vendorCode).name;

    if (this.vendors.valid && vendor.name !== existingVendor) {
      const siteId = +this.route.snapshot.params.sid;
      const vendorChangeText = `from ${existingVendor} to ${vendor.name}`;

      const data: DialogOptions = {
        data: {
          siteId,
          vendorCode: vendor.code,
          vendorChangeText
        }
      };
      this.dialog.open(ChangeVendorNoteComponent, { data }).afterClosed()
        .subscribe((vendorChanged: boolean) => {
          if (vendorChanged) {
            this.refresh.next(true);
            this.site.healthAuthorityVendor.vendorCode = vendor.code;
          }
        });
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.busy = this.healthAuthorityResource
      .getHealthAuthorityAdminSite(+this.route.snapshot.params.haid, +this.route.snapshot.params.sid)
      .pipe(
        mergeMap((site: HealthAuthoritySiteAdmin) => {
          this.site = site;
          return this.healthAuthorityResource
            .getHealthAuthorityById(this.site.healthAuthorityOrganizationId);
        }),
      )
      .subscribe((hao: HealthAuthority) => {
        let careTypeVendors = hao.careTypes.find(t => t.careType === this.site.healthAuthorityCareType.careType).vendors;
        this.healthAuthorityVendors = this.configService.vendors
          .filter((vendorConfig: VendorConfig) => careTypeVendors.findIndex(v => v.vendorCode === vendorConfig.code) >= 0) as HealthAuthorityVendorMap[];

        const vendor = this.healthAuthorityVendors
          .find((vendorConfig: VendorConfig) =>
            vendorConfig.code == this.site.healthAuthorityVendor.vendorCode
          );
        this.initForm(this.site, vendor);
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
      ],
      vendors: ['', [Validators.required]]
    });
  }

  private initForm({ pec }: HealthAuthoritySiteAdmin, vendor: VendorConfig): void {
    this.form.patchValue({ pec, 'vendors': vendor });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.route.snapshot.params.sid, value);
  }
}
