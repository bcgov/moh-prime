import { Injectable } from '@angular/core';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { Banner, BannerViewModel } from '@shared/models/banner.model';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BannerResourceService {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createOrUpdateEnrolmentLandingBanner(banner: Banner): Observable<Banner> {
    return this.apiResource.put<BannerViewModel>(`banners/enrolment-landing`, BannerViewModel.fromBanner(banner))
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap(() => this.toastService.openSuccessToast('Banner has been created/updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be created/updated');
          this.logger.error('[Banner] BannerResource::createOrUpdateEnrolmentLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public createOrUpdateSiteLandingBanner(banner: Banner): Observable<Banner> {
    return this.apiResource.put<BannerViewModel>(`banners/site-landing`, BannerViewModel.fromBanner(banner))
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap(() => this.toastService.openSuccessToast('Banner has been created/updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be created/updated');
          this.logger.error('[Banner] BannerResource::createOrUpdateSiteLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteEnrolmentLandingBanner(): NoContent {
    return this.apiResource.delete<BannerViewModel>(`banners/enrolment-landing`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Enrolment Landing Banner has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment Landing Banner could not be deleted');
          this.logger.error('[Banner] BannerResource::deleteEnrolmentLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSiteLandingBanner(): NoContent {
    return this.apiResource.delete<BannerViewModel>(`banners/site-landing`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Site Landing Banner has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Landing Banner could not be deleted');
          this.logger.error('[Banner] BannerResource::deleteSiteLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentLandingBanner(): Observable<Banner> {
    return this.apiResource.get<BannerViewModel>(`banners/enrolment-landing`)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap((banner: Banner) => this.logger.info('ENROLMENT_LANDING_BANNER', banner)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getEnrolmentLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteLandingBanner(): Observable<Banner> {
    return this.apiResource.get<BannerViewModel>(`banners/site-landing`)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap((banner: Banner) => this.logger.info('SITE_LANDING_BANNER', banner)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getSiteLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getActiveBannerByLocationCode(locationCode: BannerLocationCode): Observable<Banner> {
    const params = this.apiResourceUtilsService.makeHttpParams({ locationCode });
    return this.apiResource.get<BannerViewModel>(`banners/active`, params)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap((banner: Banner) => this.logger.info('ACTIVE_BANNER', banner)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getActiveBannerByLocationCode error has occurred: ', error);
          throw error;
        })
      );
  }
}
