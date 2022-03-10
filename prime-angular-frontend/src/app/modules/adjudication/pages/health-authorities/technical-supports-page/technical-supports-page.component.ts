import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormArray, FormBuilder, FormControl } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';

import { Observable, pipe, UnaryFunction } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { AbstractContactsPage } from '@lib/classes/abstract-contacts-page.class';
import { ConfigService } from '@config/config.service';
import { VendorConfig } from '@config/config.model';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';
import { TechnicalSupportsFormState } from '@lib/classes/technical-supports-form-state';

@Component({
  selector: 'app-technical-supports-page',
  templateUrl: './technical-supports-page.component.html',
  styleUrls: ['./technical-supports-page.component.scss']
})
export class TechnicalSupportsPageComponent extends AbstractContactsPage implements OnInit {

  public healthAuthority: HealthAuthority;

  constructor(
    protected route: ActivatedRoute,
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected fb: FormBuilder,
    protected healthAuthResource: HealthAuthorityResource,
    protected utilsService: UtilsService,
    protected configService: ConfigService,
    router: Router
  ) {
    super(route, dialog, formUtilsService, fb, healthAuthResource, utilsService, router);

    this.backRoute = AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICE;
    this.nextRoute = AdjudicationRoutes.HEALTH_AUTH_ADMINISTRATORS;
  }

  public VendorName(vendorCode: number): string {
    let matches = this.configService.vendors
      .filter((vendorConfig: VendorConfig) => vendorConfig.code === vendorCode)
    return matches ? matches[0].name : '';
  }

  public get vendors(): FormArray {
    return this.formState.form.get('vendors') as FormArray;
  }

  public onNoVendors(change: MatCheckboxChange): void {
    this.vendors.controls.forEach((vendorCheckbox: FormControl) => {
      vendorCheckbox.setValue(false);
    });
  }

  // TODO:
  public isVendorSelected(healthAuthorityVendor: HealthAuthorityVendor): boolean {
    console.log(`healthAuthorityVendor.vendorCode=${healthAuthorityVendor.vendorCode}`);
    console.log(`this.healthAuthority.technicalSupports[this.editedContactIndex].vendorsSupported=${this.healthAuthority.technicalSupports[this.editedContactIndex].vendorsSupported}`)
    let result = this.editedContactIndex != this.unsavedContactIndexValue ?
      this.healthAuthority.technicalSupports[this.editedContactIndex].vendorsSupported.includes(healthAuthorityVendor.vendorCode) :
      false;
    console.log(`result=${result}`);
    return result;
  }

  public ngOnInit(): void {
    this.cardTitlePrefix = 'Technical Support: ';
    this.init();

    this.busy = this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe((healthAuthority: HealthAuthority) => {
        this.healthAuthority = healthAuthority;
        // TODO:
        console.log("After this.healthAuthResource.getHealthAuthorityById ------------------------------------");
        this.healthAuthority.vendors.forEach((haVendor: HealthAuthorityVendor) => this.vendors.push(
          this.fb.control(false, [])
        ))
      });
  }

  protected createFormInstance(): void {
    this.formState = new TechnicalSupportsFormState(this.fb, this.formUtilsService);
  }

  protected getContactsPipe(): UnaryFunction<Observable<HealthAuthority>, Observable<Contact[]>> {
    return pipe(map(({ technicalSupports }: HealthAuthority) => technicalSupports));
  }

  protected performSubmissionRequest(contact: Contact[]): NoContent {
    return this.healthAuthResource.updateHealthAuthorityTechnicalSupports(this.route.snapshot.params.haid, contact);
  }
}
