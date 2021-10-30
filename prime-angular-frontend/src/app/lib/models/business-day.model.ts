import { WeekDay } from '@angular/common';

import { BusinessDayHours } from './business-day-hours.model';
import { DateUtils } from '@lib/utils/date-utils.class';

export class BusinessDay extends BusinessDayHours {
  public day: WeekDay;

  constructor(
    day: WeekDay = null,
    startTime: string = null,
    endTime: string = null
  ) {
    super(startTime, endTime);
    this.day = day;
  }

  // TODO move conversion to the resource so hours and minutes is client model, or
  //      even better move this up to the API and make automapper do the conversion
  public static asHoursAndMins({ day, startTime, endTime }: BusinessDay): BusinessDay {
    return new BusinessDay(day, DateUtils.fromTimespan(startTime), DateUtils.fromTimespan(endTime));
  };

  // TODO move conversion to the resource so hours and minutes is client model, or
  //      even better move this up to the API and make automapper do the conversion
  public static asTimespan({ day, startTime, endTime }: BusinessDay): BusinessDay {
    return new BusinessDay(day, DateUtils.toTimespan(startTime), DateUtils.toTimespan(endTime));
  };
}
