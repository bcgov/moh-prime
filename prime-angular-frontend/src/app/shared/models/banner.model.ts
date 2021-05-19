import { BannerType } from '@shared/enums/banner-type.enum';
import moment, { Moment } from 'moment';

export interface Banner {
  bannerType: BannerType;
  title: string;
  content: string; // HTML markup
  startDate: string;
  startTime: string;
  endDate: string;
  endTime: string;
}

export class BannerViewModel {
  public constructor(
    public bannerType: BannerType,
    public title: string,
    public content: string,
    public startDate: Moment,
    public endDate: Moment
  ) {
    this.bannerType = bannerType;
    this.title = title;
    this.content = content;
    this.startDate = startDate;
    this.endDate = endDate;
  }

  public static toBanner(bannerVm: BannerViewModel): Banner {
    if (!bannerVm) {
      return null;
    }

    const start = moment(bannerVm.startDate).local();
    const end = moment(bannerVm.endDate).local();

    return {
      bannerType: bannerVm.bannerType,
      title: bannerVm.title,
      content: bannerVm.content,
      startDate: start.format(),
      startTime: start.format("HHmm"),
      endDate: end.format(),
      endTime: end.format("HHmm")
    };
  }

  public static fromBanner({ bannerType, title, content, startDate, startTime, endDate, endTime }: Banner): BannerViewModel {
    const parsedStartTime = moment(startTime, 'HHmm');
    const parsedEndTime = moment(endTime, 'HHmm');

    const startDateTime = moment(startDate).set({
      hour: parsedStartTime.get('hour'),
      minute: parsedStartTime.get('minute')
    });
    const endDateTime = moment(endDate).set({
      hour: parsedEndTime.get('hour'),
      minute: parsedEndTime.get('minute')
    });

    return new BannerViewModel(bannerType, title, content, startDateTime, endDateTime);
  }
}
