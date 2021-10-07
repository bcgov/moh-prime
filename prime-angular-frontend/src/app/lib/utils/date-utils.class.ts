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
}
