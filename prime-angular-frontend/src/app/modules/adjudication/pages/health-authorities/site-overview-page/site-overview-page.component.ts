import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
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
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Contact } from '@lib/models/contact.model';

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
  public form: UntypedFormGroup;
  public healthAuthorityVendors: HealthAuthorityVendorMap[];
  public refresh: BehaviorSubject<boolean>;
  public pharmanetAdministrators: Contact[];
  public technicalSupports: Contact[];

  constructor(
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    private formUtilsService: FormUtilsService,
    private fb: UntypedFormBuilder,
    private configService: ConfigService,
    private siteResource: SiteResource,
    private dialog: MatDialog,
  ) {
    this.refresh = new BehaviorSubject<boolean>(null);
  }

  public get pec(): UntypedFormControl {
    return this.form.get('pec') as UntypedFormControl;
  }

  public get vendors(): UntypedFormControl {
    return this.form.get('vendors') as UntypedFormControl;
  }

  public saveSiteId(): void {
    if (this.pec.valid) {
      const siteId = +this.route.snapshot.params.sid;
      const pec = this.form.value.pec;

      this.siteResource.pecExistsWithinHa(siteId, pec).subscribe((result) => {
        if (result) {
          const data: DialogOptions = {
            title: 'Site ID is in use',
            message: 'This Site ID is already in use in your organization.  Allow multiple sites?',
            actionText: "Yes"
          };
          this.busy = this.dialog.open(ConfirmDialogComponent, { data })
            .afterClosed()
            .subscribe((result) => {
              if (result) {
                this.busy = this.siteResource.updatePecCode(siteId, pec)
                  .subscribe(() => {
                    this.refresh.next(true);
                    this.site.pec = pec;
                  });
              } else {
                this.pec.setValue(this.site.pec);
              }
            });
        } else {
          this.busy = this.siteResource.updatePecCode(siteId, pec)
            .subscribe(() => {
              this.refresh.next(true);
              this.site.pec = pec;
            });
        }
      });

    }
  }

  public onEditVendor(): void {
    const siteId = +this.route.snapshot.params.sid;
    //const vendorChangeText = `from ${existingVendor} to ${vendor.name}`;

    const data: DialogOptions = {
      data: {
        siteId,
        vendorCode: this.site.healthAuthorityVendor.vendorCode,
        siteVendors: this.healthAuthorityVendors,
      }
    };
    this.dialog.open(ChangeVendorNoteComponent, { data }).afterClosed()
      .subscribe((data) => {
        if (data?.result) {
          this.refresh.next(true);
          this.site.healthAuthorityVendor.vendorCode = data.vendorCode;
        }
      });
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
        this.pharmanetAdministrators = hao.pharmanetAdministrators;
        this.technicalSupports = hao.technicalSupports;
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
