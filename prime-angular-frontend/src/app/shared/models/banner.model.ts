import { BannerType } from '@shared/enums/banner-type.enum';

export interface Banner {
  bannerType: BannerType;
  title: string;
  content: string; // HTML markup
  startDate: string;
  endDate: string;
}
