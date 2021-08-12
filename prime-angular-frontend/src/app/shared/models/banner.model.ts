import { BannerType } from '@shared/enums/banner-type.enum';
import moment, { Moment } from 'moment';

export interface Banner {
  id: number;
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
    public id: number,
    public bannerType: BannerType,
    public title: string,
    public content: string,
    public startTimestamp: Moment,
    public endTimestamp: Moment
  ) {
    this.id = id;
    this.bannerType = bannerType;
    this.title = title;
    this.content = content;
    this.startTimestamp = startTimestamp;
    this.endTimestamp = endTimestamp;
  }

  public static toBanner(bannerVm: BannerViewModel): Banner | null {
    if (!bannerVm) {
      return null;
    }

    const start = moment(bannerVm.startTimestamp).local();
    const end = moment(bannerVm.endTimestamp).local();

    return {
      id: bannerVm.id,
      bannerType: bannerVm.bannerType,
      title: bannerVm.title,
      content: bannerVm.content,
      startDate: start.format(),
      startTime: start.format('HHmm'),
      endDate: end.format(),
      endTime: end.format('HHmm')
    };
  }

  public static fromBanner({ id, bannerType, title, content, startDate, startTime, endDate, endTime }: Banner): BannerViewModel {
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

    return new BannerViewModel(id, bannerType, title, content, startDateTime, endDateTime);
  }
}
