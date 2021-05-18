import { Moment } from 'moment';
import { BannerType } from '@shared/enums/banner-type.enum';
import moment from 'moment';
// import * as moment from 'moment';

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
  public bannerType: BannerType;
  public title: string;
  public content: string;
  public startDate: Moment;
  public endDate: Moment;

  public static toBanner(bannerVm: BannerViewModel): Banner {
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

  public static fromBanner(banner: Banner): BannerViewModel {
    const startDate = moment(banner.startDate);
    const startTime = moment(banner.startTime, 'HHmm');
    const endDate = moment(banner.endDate);
    const endTime = moment(banner.endTime, 'HHmm');

    const start = startDate.set({
      hour: startTime.get('hour'),
      minute: startTime.get('minute')
    });
    const end = endDate.set({
      hour: endTime.get('hour'),
      minute: endTime.get('minute')
    });

    const vm = new BannerViewModel();
    vm.bannerType = banner.bannerType;
    vm.title = banner.title;
    vm.content = banner.content;
    vm.startDate = start;
    vm.endDate = end;
    return vm;
  }
}
