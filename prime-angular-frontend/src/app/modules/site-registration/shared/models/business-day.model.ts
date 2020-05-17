import { WeekDay } from '@angular/common';

export class BusinessDayHours {
  public startTime: string;
  public endTime: string;

  // TODO should a default for hours be set
  constructor(startTime: string = '9', endTime: string = '17') {
    this.startTime = startTime;
    this.endTime = endTime;
  }
}

export class BusinessDay extends BusinessDayHours {
  public day: WeekDay;

  constructor(day: WeekDay = null, startTime: string = null, endTime: string = null) {
    super(startTime, endTime);
    this.day = day;
  }
}
