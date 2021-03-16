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

  public createBanner(banner: Banner): Observable<Banner> {
    return this.apiResource.post<Banner>(`banners`, banner)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap((newBanner: Banner) => {
          this.toastService.openSuccessToast('Banner has been created');
          this.logger.info('NEW_BANNER', newBanner);
        }),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be created');
          this.logger.error('[Banner] BannerResource::createBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateBanner(bannerId: number, banner: Banner): Observable<Banner> {
    return this.apiResource.put<Banner>(`banners/${bannerId}`, banner)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap(() => this.toastService.openSuccessToast('Banner has been updated')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be updated');
          this.logger.error('[Banner] BannerResource::updateBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public deleteBanner(bannerId: number): Observable<Banner> {
    return this.apiResource.delete<Banner>(`banners/${bannerId}`)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap(() => this.toastService.openSuccessToast('Banner has been deleted')),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be deleted');
          this.logger.error('[Banner] BannerResource::deleteBanner error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBanners(): Observable<Banner[]> {
    return this.apiResource.get<Banner[]>(`banners`)
      .pipe(
        map((response: ApiHttpResponse<Banner[]>) => response.result),
        tap((banners: Banner[]) => this.logger.info('BANNERS', banners)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banners could not be retrieved');
          this.logger.error('[Banner] BannerResource::getBanners error has occurred: ', error);
          throw error;
        })
      );
  }

  public getBannerById(bannerId: number): Observable<Banner> {
    return this.apiResource.get<Banner>(`banners/${bannerId}`)
      .pipe(
        map((response: ApiHttpResponse<Banner>) => response.result),
        tap((banner: Banner) => this.logger.info('BANNER', banner)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Banner could not be retrieved');
          this.logger.error('[Banner] BannerResource::getBannerById error has occurred: ', error);
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
