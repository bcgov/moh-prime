import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { BannerType } from '@shared/enums/banner-type.enum';

export interface Banner {
  id: number;
  bannerType: BannerType;
  bannerLocationCode: BannerLocationCode;
  title: string;
  content: string; // HTML markup
  adminId: number;
  startDate: string;
  endDate: string;
}
