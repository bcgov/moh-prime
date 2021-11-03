import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { NoContent } from '@core/resources/abstract-resource';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { MINIMUM_AGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractSiteRegistrationPage } from '@registration/shared/classes/abstract-site-registration-page.class';
import { IndividualDeviceProvider } from '@registration/shared/models/individual-device-provider.model';
import { Site } from '@registration/shared/models/site.model';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { CardListItem } from '@shared/components/card-list/card-list.component';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import moment from 'moment';
import { noop, Observable, of, Subscription } from 'rxjs';
import { DeviceProviderPageFormState } from './device-provider-page-form-state.class';

@Component({
  selector: 'app-device-provider-page',
  templateUrl: './device-provider-page.component.html',
  styleUrls: ['./device-provider-page.component.scss'],
  providers: [FormatDatePipe]
})
export class DeviceProviderPageComponent extends AbstractSiteRegistrationPage implements OnInit {
  public formState: DeviceProviderPageFormState;
  public title: string;
  public isCompleted: boolean;
  public isEditing: boolean;
  public maxDateOfBirth: moment.Moment;
  public busy: Subscription;

  public deviceProviders: IndividualDeviceProvider[];
  public contactCardListItems: CardListItem[];

  public routeUtils: RouteUtils;

  private site: Site;

  constructor(
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
    this.deviceProviders = [];

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.SITES);

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');
  }

  public onBack() {
    const nextRoute = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.HOURS_OPERATION;

    this.routeUtils.routeRelativeTo(nextRoute);
  }

  public onAdd(): void {
    this.isEditing = true;
    this.formState.form.reset({ id: 0 });
  }

  public onEdit(index: number): void {
    this.isEditing = true;
    this.formState.patchValue(this.deviceProviders[index]);
  }

  public onRemove(index: number): void {
    this.busy = this.siteResource.removeIndividualDeviceProvider(this.route.snapshot.params.sid, this.deviceProviders[index].id)
      .subscribe(() => {
        this.deviceProviders.splice(index, 1);
        this.updateCardList(this.deviceProviders);
      });
  }

  /**
   * @description
   * Continue to next view when at least one provider exists.
   */
  public onContinue(): void {
    // Protection from routing, but not necessary as the view
    // toggles when the list of contacts is empty
    if (this.deviceProviders.length) {
      this.routeToNextRoute();
    }
  }

  ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected performSubmission(): Observable<IndividualDeviceProvider | NoContent> {
    if (this.isCompleted) {
      // Store in form state and don't save
    }

    const createOrUpdateModel = this.formState.json;
    const siteId = this.route.snapshot.params.sid;
    if (createOrUpdateModel.id) {
      return this.siteResource.updateIndividualDeviceProvider(siteId, createOrUpdateModel.id, createOrUpdateModel);
    } else {
      return this.siteResource.createIndividualDeviceProvider(siteId, createOrUpdateModel);
    }
  }

  protected afterSubmitIsSuccessful(provider?: IndividualDeviceProvider): void {
    if (provider) {
      this.deviceProviders.push(this.formState.json);
    } else {
      const update = this.formState.json;
      this.deviceProviders = this.deviceProviders.map((provider: IndividualDeviceProvider) => (update.id === provider.id) ? update : provider);
    }
    this.updateCardList(this.deviceProviders);
    this.isEditing = false;
  }

  /**
   * @description
   * Update the contact card list.
   */
  private updateCardList(deviceProviders: IndividualDeviceProvider[]): void {
    this.contactCardListItems = this.getCardList(deviceProviders);

    if (!deviceProviders.length) {
      this.isEditing = true;
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

  protected createFormInstance() {
    this.formState = this.siteFormStateService.deviceProviderFormState;
  }

  protected patchForm(): void {
    this.site = this.siteService.site;
    this.isCompleted = this.site?.completed;
    this.siteFormStateService.setForm(this.site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();

    this.isEditing = true;
  }

  private routeToNextRoute(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.ADMINISTRATOR;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
