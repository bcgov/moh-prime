import { Injectable } from '@angular/core';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { Banner } from '@shared/models/banner.model';
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
    return this.apiResource.put<Banner>(`banners/enrolment-landing`, banner)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap(() => this.toastService.openSuccessToast('Banner has been created/updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be created/updated');
          this.logger.error('[Banner] BannerResource::createOrUpdateEnrolmentLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public createOrUpdateSiteLandingBanner(banner: Banner): Observable<Banner> {
    return this.apiResource.put<Banner>(`banners/site-landing`, banner)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap(() => this.toastService.openSuccessToast('Banner has been created/updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be created/updated');
          this.logger.error('[Banner] BannerResource::createOrUpdateSiteLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteEnrolmentLandingBanner(): Observable<Banner> {
    return this.apiResource.delete<Banner>(`banners/enrolment-landing`)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap(() => this.toastService.openSuccessToast('Enrolment Landing Banner has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment Landing Banner could not be deleted');
          this.logger.error('[Banner] BannerResource::deleteEnrolmentLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteSiteLandingBanner(): Observable<Banner> {
    return this.apiResource.delete<Banner>(`banners/site-landing`)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap(() => this.toastService.openSuccessToast('Site Landing Banner has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Landing Banner could not be deleted');
          this.logger.error('[Banner] BannerResource::deleteSiteLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentLandingBanner(): Observable<Banner> {
    return this.apiResource.get<Banner>(`banners/enrolment-landing`)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap((banner: Banner) => this.logger.info('ENROLMENT_LANDING_BANNER', banner)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getEnrolmentLandingBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteLandingBanner(): Observable<Banner> {
    return this.apiResource.get<Banner>(`banners/site-landing`)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
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
    return this.apiResource.get<Banner>(`banners/active`, params)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap((banner: Banner) => this.logger.info('ACTIVE_BANNER', banner)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getActiveBannerByLocationCode error has occurred: ', error);
          throw error;
        })
      );
  }
}
