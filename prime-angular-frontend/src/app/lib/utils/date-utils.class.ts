import moment, { Moment } from 'moment';

import { RENEWAL_PERIOD } from '@lib/constants';

export class DateUtils {
  /**
   * @description
   * Check that a date is within a number of days before another date.
   */
  public static withinDaysBeforeDate(
    date: string | Moment | null,
    daysBeforeDate: number,
    todayOrOtherDate: string | Moment = moment()
  ): boolean {
    if (!date || !todayOrOtherDate) {
      return false;
    }

    const minusDaysBeforeDate = moment(date).subtract(daysBeforeDate, 'days');
    return moment(todayOrOtherDate).isAfter(minusDaysBeforeDate);
  }

  /**
   * @description
   * Check that a date is within the 90 day renewal period.
   */
  public static withinRenewalPeriod(date: string | Moment | null): boolean {
    return DateUtils.withinDaysBeforeDate(date, RENEWAL_PERIOD);
  }

  /**
   * @description
   * Check that a date falls within a date range, which forces the
   * start and end dates .
   */
  public static isWithinDateRange(
    date: Moment,
    startDate: Moment,
    endDate: Moment,
    explicitRange: boolean = true
  ): boolean {
    if (!date || !startDate || (!endDate && explicitRange)) {
      throw new Error('Undefined required argument(s)');
    }

    if (!endDate) {
      endDate = startDate;
    }

    // Ensure the date can fall completely within the range
    startDate = startDate.clone().startOf('day').subtract(1, 'second');
    endDate = endDate.clone().endOf('day').add(1, 'second');

    return date.isBetween(startDate, endDate);
  }

  /**
   * @description
   * Convert timespan to hours and minutes.
   */
  public static fromTimespan(time: string): string {
    return (moment.duration(time).asHours() === 24)
      ? '24:00' // Convert timespan of 1.00:00:00 (1 day) to hours and minutes
      : time.slice(0, -3);
  }

  /**
   * @description
   * Convert hours and minutes to timespan.
   */
  public static toTimespan(time: string): string {
    return (time === '24:00')
      ? '1.00:00:00' // Convert from 24 hours to 1 day (1.00:00:00)
      : `${time}:00`;
  }
}
