import { WeekDay } from '@angular/common';

import { BusinessDayHours } from './business-day-hours.model';

export class BusinessDay extends BusinessDayHours {
  public day: WeekDay;

  constructor(day: WeekDay = null, startTime: string = null, endTime: string = null) {
    super(startTime, endTime);
    this.day = day;
  }
}
