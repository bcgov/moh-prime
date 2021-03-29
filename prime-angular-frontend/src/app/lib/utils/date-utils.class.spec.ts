import moment from 'moment';

import { DateUtils } from './date-utils.class';

describe('DateUtils', () => {
  const util = DateUtils;

  beforeEach(() => { });

  it('should exist', () => expect(util).toBeTruthy());

  describe('DateUtils.isWithinDateRange', () => {
    let startOfMonth: moment.Moment;
    let endOfMonth: moment.Moment;

    const getDateRange = (start: number, end: number) => [
      startOfMonth.clone().add(start, 'day'),
      endOfMonth.clone().add(end, 'day')
    ];

    beforeAll(() => {
      startOfMonth = moment().startOf('month');
      endOfMonth = moment().endOf('month');
    });

    it('should be within date range when date is same as start date at start of day', () => {
      const [startDate, endDate] = getDateRange(5, 10);
      const selectedDate = startDate.clone().startOf('day');
      const result = util.isWithinDateRange(selectedDate, startDate, endDate);
      expect(result).toBeTruthy();
    });

    it('should be within date range when date is same as end date at end of day', () => {
      const [startDate, endDate] = getDateRange(5, 10);
      const selectedDate = endDate.clone().endOf('day');
      const result = util.isWithinDateRange(selectedDate, startDate, endDate);
      expect(result).toBeTruthy();
    });

    it('should not be within date range when before start date by a second', () => {
      const [startDate, endDate] = getDateRange(5, 10);
      const selectedDate = startDate.clone().startOf('day').subtract(1, 'second');
      const result = util.isWithinDateRange(selectedDate, startDate, endDate);
      expect(result).toBeFalsy();
    });

    it('should not be within date range when after end date by a second', () => {
      const [startDate, endDate] = getDateRange(5, 10);
      const selectedDate = endDate.clone().endOf('day').add(1, 'second');
      const result = util.isWithinDateRange(selectedDate, startDate, endDate);
      expect(result).toBeFalsy();
    });

    it('should throw when selected date not provided', () => {
      const [startDate, endDate] = getDateRange(5, 10);
      expect(() => util.isWithinDateRange(null, startDate, endDate)).toThrow();
    });

    it('should throw when start date is not provided', () => {
      const [_, endDate] = getDateRange(5, 10);
      const selectedDate = _.clone();
      expect(() => util.isWithinDateRange(selectedDate, null, endDate)).toThrow();
    });

    it('should throw when end date is not provided, and an explicit range is required', () => {
      const [startDate, _] = getDateRange(5, 10);
      const selectedDate = startDate.clone();
      expect(() => util.isWithinDateRange(selectedDate, startDate, null)).toThrow();
    });

    it('should be within date range when no end date is provided, and an explicit range is not required', () => {
      const [startDate, _] = getDateRange(5, 10);
      const selectedDate = startDate.clone();
      const result = util.isWithinDateRange(selectedDate, startDate, null, false);
      expect(result).toBeTruthy();
    });
  });
});
