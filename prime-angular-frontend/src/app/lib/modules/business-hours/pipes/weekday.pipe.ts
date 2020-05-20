import { Pipe, PipeTransform } from '@angular/core';
import { WeekDay } from '@angular/common';

import * as moment from 'moment';

@Pipe({
  name: 'weekday'
})
export class WeekdayPipe implements PipeTransform {
  public transform(value: WeekDay, output: 'full' | 'short' | 'min' = 'full'): string {
    return (output === 'short')
      ? moment.weekdaysShort(value)
      : (output === 'min')
        ? moment.weekdaysMin(value)
        : moment.weekdays(value);
  }
}
