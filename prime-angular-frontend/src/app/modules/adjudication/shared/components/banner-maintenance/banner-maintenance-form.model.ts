import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { BannerType } from '@shared/enums/banner-type.enum';

export interface BannerMaintenanceForm {
  id: number;
  title: string;
  content: string;
  bannerType: BannerType;
  bannerLocationCode: BannerLocationCode;
  startDate: string;
  startTime: string;
  endDate: string;
  endTime: string;
}
