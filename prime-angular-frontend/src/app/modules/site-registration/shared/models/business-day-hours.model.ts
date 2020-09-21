import * as moment from 'moment';

export class BusinessDayHours {
  public startTime: string;
  public endTime: string;

  constructor(startTime: string, endTime: string) {
    this.startTime = startTime;
    this.endTime = endTime;
  }

  /**
   * @description
   * Convert timespan to hours and minutes.
   */
  public static fromTimeSpan(time: string) {
    return (moment.duration(time).asHours() === 24)
      ? '24:00' // Convert timespan of 1.00:00:00 (1 day) to hours and minutes
      : time.slice(0, -3);
  }

  /**
   * @description
   * Convert hours and minutes to timespan.
   */
  public static toTimespan(time: string) {
    return (time === '24:00')
      ? time = '1.00:00:00' // Convert to 24 hours to 1 day (1.00:00:00)
      : `${time}:00`;
  }
}
