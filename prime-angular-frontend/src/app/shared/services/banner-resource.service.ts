import { Injectable } from '@angular/core';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
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
    private logger: ConsoleLoggerService
  ) { }

  public createBanner(locationCode: BannerLocationCode, banner: Banner): Observable<Banner> {
    const params = this.apiResourceUtilsService.makeHttpParams({ locationCode });
    return this.apiResource.post<BannerViewModel>(`banners`, BannerViewModel.fromBanner(banner), params)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap(() => this.toastService.openSuccessToast('Banner has been created')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be created');
          this.logger.error('[Banner] BannerResource::createBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateBanner(bannerId: number, banner: Banner): Observable<Banner> {
    return this.apiResource.put<BannerViewModel>(`banners/${bannerId}`, BannerViewModel.fromBanner(banner))
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap(() => this.toastService.openSuccessToast('Banner has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be updated');
          this.logger.error('[Banner] BannerResource::updateBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteBanner(bannerId: number): NoContent {
    return this.apiResource.delete<BannerViewModel>(`banners/${bannerId}`)
      .pipe(
        NoContentResponse,
        tap(() => this.toastService.openSuccessToast('Banner has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be deleted');
          this.logger.error('[Banner] BannerResource::deleteBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBanner(bannerId: number): Observable<Banner> {
    return this.apiResource.get<BannerViewModel>(`banners/${bannerId}`)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel>) => BannerViewModel.toBanner(response.result)),
        tap((banners: Banner) => this.logger.info('BANNER', banners)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEnrolmentLandingBanners(): Observable<Banner[]> {
    return this.apiResource.get<BannerViewModel[]>(`banners/enrolment-landing`)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel[]>) => response.result.map(b => BannerViewModel.toBanner(b))),
        tap((banners: Banner[]) => this.logger.info('ENROLMENT_LANDING_BANNERS', banners)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Enrolment Landing Banners could not be retrieved');
          this.logger.error('[Banner] BannerResource::getEnrolmentLandingBanners error has occurred: ', error);
          throw error;
        })
      );
  }

  public getSiteLandingBanners(): Observable<Banner[]> {
    return this.apiResource.get<BannerViewModel[]>(`banners/site-landing`)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel[]>) => response.result.map(b => BannerViewModel.toBanner(b))),
        tap((banners: Banner[]) => this.logger.info('SITE_LANDING_BANNERS', banners)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Site Landing Banners could not be retrieved');
          this.logger.error('[Banner] BannerResource::getSiteLandingBanners error has occurred: ', error);
          throw error;
        })
      );
  }

  public getActiveBannersByLocationCode(locationCode: BannerLocationCode): Observable<Banner[]> {
    const params = this.apiResourceUtilsService.makeHttpParams({ locationCode });
    return this.apiResource.get<BannerViewModel[]>(`banners/active`, params)
      .pipe(
        map((response: ApiHttpResponse<BannerViewModel[]>) => response.result.map(b => BannerViewModel.toBanner(b))),
        tap((banners: Banner[]) => this.logger.info('ACTIVE_BANNERS', banners)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banners could not be retrieved');
          this.logger.error('[Banner] BannerResource::getActiveBannersByLocationCode error has occurred: ', error);
          throw error;
        })
      );
  }
}
