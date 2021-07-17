import moment, { Moment } from 'moment';

export class DateUtils {
  /**
   * @description
   * Check that a date is within a number of days before another date.
   */
  public static withinDaysBeforeDate(
    date: string | Moment,
    daysBeforeDate: number,
    todayOrOtherDate: string | Moment = moment()
  ) {
    const minusDaysBeforeDate = this.toMoment(date).subtract(daysBeforeDate, 'days');
    return this.toMoment(todayOrOtherDate).isAfter(minusDaysBeforeDate);
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
   * When a date is not already a moment then convert it
   * to a moment to allow for method chaining.
   */
  public static toMoment(date: string | Moment): Moment {
    return (typeof date === 'string') ? moment(date) : date;
  }
}
