import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';

import { Observable, pipe, UnaryFunction } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { AbstractContactsPage } from '@lib/classes/abstract-contacts-page.class';
import { TechnicalSupportsFormState } from '@lib/classes/technical-supports-form-state';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { CardListItem } from '@shared/components/card-list/card-list.component';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { HealthAuthorityTechnicalSupport } from '@shared/models/health-authority-technical-support';
import { HealthAuthorityVendorPipe } from '@shared/pipes/health-authority-vendor.pipe';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';

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
    protected fb: UntypedFormBuilder,
    protected healthAuthResource: HealthAuthorityResource,
    protected utilsService: UtilsService,
    protected configService: ConfigService,
    router: Router
  ) {
    super(route, dialog, formUtilsService, fb, healthAuthResource, utilsService, router);

    this.backRoute = AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICE;
    this.nextRoute = AdjudicationRoutes.HEALTH_AUTH_ADMINISTRATORS;
  }


  public get vendors(): UntypedFormArray {
    return this.formState.form.get('vendors') as UntypedFormArray;
  }

  public get anyVendorsChecked(): boolean {
    return this.vendors.controls.some((vendorCheckbox: UntypedFormControl) => vendorCheckbox.value);
  }

  public onNoVendors(change: MatCheckboxChange): void {
    this.vendors.controls.forEach((vendorCheckbox: UntypedFormControl) => {
      vendorCheckbox.setValue(false);
    });
  }

  public ngOnInit(): void {
    this.cardTitlePrefix = 'Technical Support: ';
    this.init();
  }

  public onEdit(contactIndex: number): void {
    super.onEdit(contactIndex);

    const vendorsCheckState: boolean[] = [];
    this.healthAuthority.vendors.forEach((haVendor: HealthAuthorityVendor) => {
      vendorsCheckState.push(this.healthAuthority.technicalSupports[contactIndex].vendorsSupported.includes(haVendor.vendorCode));
    });
    this.vendors.patchValue(vendorsCheckState);
  }

  protected createFormInstance(): void {
    this.formState = new TechnicalSupportsFormState(this.fb, this.formUtilsService);
  }

  /**
   * This gets executed during `ngOnInit` and after saving Contact
   */
  protected getContactsPipe(): UnaryFunction<Observable<HealthAuthority>, Observable<Contact[]>> {
    return pipe(
      tap((healthAuthority: HealthAuthority) => {
        this.healthAuthority = healthAuthority;
        this.vendors.controls.length = 0;
        this.healthAuthority.vendors.forEach((haVendor: HealthAuthorityVendor) => this.vendors.push(
          this.fb.control(false, []))
        );
      }),
      map(({ technicalSupports }: HealthAuthority) => technicalSupports));
  }

  protected performSubmissionRequest(contacts: HealthAuthorityTechnicalSupport[]): NoContent {
    return this.healthAuthResource.updateHealthAuthorityTechnicalSupports(this.route.snapshot.params.haid, contacts);
  }

  protected manipulateJsonPreSubmission(json: any): any {
    const selectedVendorCodes: number[] = [];
    this.healthAuthority.vendors.forEach((haVendor: HealthAuthorityVendor, i: number) => {
      if (json.vendors[i]) {
        // If checkbox checked, add corresponding vendor code
        selectedVendorCodes.push(haVendor.vendorCode);
      }
    });
    // Adjust JSON to match shape of HealthAuthorityTechnicalSupport class
    json.vendorsSupported = selectedVendorCodes;
    return json;
  }

  protected getContactListItem(): (contact: HealthAuthorityTechnicalSupport) => CardListItem {
    const healthAuthorityVendorPipe = new HealthAuthorityVendorPipe(this.configService);
    return (contact: HealthAuthorityTechnicalSupport) => ({
      icon: 'account_circle',
      title: `${this.cardTitlePrefix}${contact.firstName} ${contact.lastName}`,
      properties: [
        { key: 'Job Title', value: contact.jobRoleTitle },
        { key: 'Vendor(s)', value: healthAuthorityVendorPipe.transform(contact.vendorsSupported) }
      ],
      action: {
        title: 'Update Information'
      }
    });
  }
}
