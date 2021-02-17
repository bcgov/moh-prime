import { WeekdayPipe } from './weekday.pipe';

import * as moment from 'moment';

describe('WeekdayPipe', () => {
  let pipe: WeekdayPipe;
  const weekDays = [...Array(7).keys()].map(i => i);

  beforeEach(() => pipe = new WeekdayPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should convert WeekDay of 0 through 6 to full day of week Sunday through Saturday', () => {
    const results = weekDays.map((wd: number) => pipe.transform(wd, 'full'));
    results.forEach((result, i) => expect(result).toBe(moment.weekdays(i)));
  });

  it('should convert WeekDay of 0 through 6 to short day of week Sun through Sat', () => {
    const results = weekDays.map((wd: number) => pipe.transform(wd, 'short'));
    results.forEach((result, i) => expect(result).toBe(moment.weekdaysShort(i)));
  });

  it('should convert WeekDay of 0 through 6 to min day of week Su through Sa', () => {
    const results = weekDays.map((wd: number) => pipe.transform(wd, 'min'));
    results.forEach((result, i) => expect(result).toBe(moment.weekdaysMin(i)));
  });
});
