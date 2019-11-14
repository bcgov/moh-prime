import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

import { APP_DATE_FORMAT } from '@shared/modules/ngx-material/ngx-material.module';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {
  transform(date: string, format: string = APP_DATE_FORMAT): string {
    if (date) {
      date = moment(date).format(format);
    }

    return date;
  }
}
