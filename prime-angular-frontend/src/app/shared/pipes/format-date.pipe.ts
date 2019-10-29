import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {
  transform(date: string, format: string = 'MMM D, Y'): string {
    if (date) {
      date = moment(date).format(format);
    }

    return date;
  }
}
