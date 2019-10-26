import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {

  transform(date: any, format: string = 'MMM d, Y'): any {
    if (date) {
      date = moment(date).format(format);
    }

    return date;
  }

}
