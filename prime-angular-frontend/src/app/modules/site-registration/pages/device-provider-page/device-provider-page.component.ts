import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup } from '@angular/forms';
import moment from 'moment';

import { MINIMUM_AGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';

import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { CardListItem } from '@shared/components/card-list/card-list.component';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { IndividualDeviceProvider } from '@registration/shared/models/individual-device-provider.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { DeviceProviderPageFormState } from './device-provider-page-form-state.class';

@Component({
  selector: 'app-device-provider-page',
  templateUrl: './device-provider-page.component.html',
  styleUrls: ['./device-provider-page.component.scss'],
  providers: [FormatDatePipe]
})
export class DeviceProviderPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: DeviceProviderPageFormState;
  public form: FormGroup;

  public currentIndex: number;

  public title: string;
  public isCompleted: boolean;
  public isEditing: boolean;
  public maxDateOfBirth: moment.Moment;
  public busy: Subscription;

  public contactCardListItems: CardListItem[];

  public routeUtils: RouteUtils;

  private site: Site;

  constructor(
    protected fb: FormBuilder,
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    private formatDatePipe: FormatDatePipe,
    protected route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService, siteService, siteFormStateService, siteResource);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');
    this.currentIndex = -1;
  }

  public onBack() {
    const nextRoute = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(nextRoute);
  }

  public onCancel() {
    this.isEditing = false;
  }

  public onAdd(): void {
    this.currentIndex = -1;
    this.form = this.formState.buildIndividualDeviceProvider();
    this.isEditing = true;
  }

  public onEdit(index: number): void {
    this.currentIndex = index;
    this.form = this.formState.at(index);
    this.isEditing = true;
  }

  public onContinue(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      if (this.currentIndex !== -1) {
        this.formState.individualDeviceProviders.at(this.currentIndex).patchValue(this.form);
      } else {
        this.formState.individualDeviceProviders.push(this.form);
      }
      this.updateCardList(this.formState.json);
      this.isEditing = false;
    }
  }

  public onRemove(index: number): void {
    this.formState.individualDeviceProviders.removeAt(index);
    this.updateCardList(this.formState.json);
  }

  ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.deviceProviderFormState;
    this.form = this.formState.buildIndividualDeviceProvider();
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.siteFormStateService.setForm(this.site, !this.hasBeenSubmitted);
    this.updateCardList(this.formState.json);
    this.isEditing = !this.formState.json.length;
  }

  /**
   * @description
   * Update the contact card list.
   */
  private updateCardList(deviceProviders: IndividualDeviceProvider[]): void {
    this.contactCardListItems = this.getCardList(deviceProviders);

    if (!deviceProviders.length) {
      this.isEditing = true;
      this.form = this.formState.buildIndividualDeviceProvider();
    }
  }

  private getCardList(deviceProviders: IndividualDeviceProvider[]): CardListItem[] {
    return deviceProviders.map(this.getListItem());
  }

  protected getListItem(): (deviceProvider: IndividualDeviceProvider) => CardListItem {
    return (deviceProvider: IndividualDeviceProvider) => ({
      icon: 'account_circle',
      title: `${deviceProvider.firstName} ${deviceProvider.lastName}`,
      properties: [
        { key: 'Date of Birth', value: this.formatDatePipe.transform(deviceProvider.dateOfBirth) },
        { key: 'Email', value: deviceProvider.email }
      ],
      action: {
        title: 'Update Information'
      }
    });
  }

  protected additionalValidityChecks(obj: { individualDeviceProviders: IndividualDeviceProvider[] }): boolean {
    // At least one provider needs to exist
    return (obj.individualDeviceProviders.length > 0);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeToNextRoute();
  }

  private routeToNextRoute(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.ADMINISTRATOR;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
