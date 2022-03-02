import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
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

  public healthAuthorityVendors: HealthAuthorityVendor[];


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

  // public get vendors(): FormGroup {
  //   return this.formState.form.get('vendors') as FormGroup;
  // }

  public ngOnInit(): void {
    this.cardTitlePrefix = 'Technical Support: ';
    this.init();

    this.busy = this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe((healthAuthority: HealthAuthority) => { this.healthAuthorityVendors = healthAuthority.vendors });
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
