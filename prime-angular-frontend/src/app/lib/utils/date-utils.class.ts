import moment from 'moment';

export class DateUtils {
  /**
   * @description
   * Check that a date falls within a date range, which forces the
   * start and end dates .
   */
  public static isWithinDateRange(date: moment.Moment, startDate: moment.Moment, endDate: moment.Moment, explicitRange: boolean = true): boolean {
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
