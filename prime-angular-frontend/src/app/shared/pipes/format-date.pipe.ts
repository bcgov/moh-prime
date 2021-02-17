import { Pipe, PipeTransform } from '@angular/core';

import * as moment from 'moment';

import { APP_DATE_FORMAT } from '@lib/modules/ngx-material/ngx-material.module';

@Pipe({
  name: 'formatDate'
})
export class FormatDatePipe implements PipeTransform {
  public transform(date: string, format: string = APP_DATE_FORMAT): string {
    return (date)
      ? moment(date).format(format)
      : date;
  }
}
