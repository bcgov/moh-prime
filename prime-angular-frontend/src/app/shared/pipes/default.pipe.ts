import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'default'
})
export class DefaultPipe implements PipeTransform {
  transform(value: any, defaultValue: string = '-'): any {
    return (value) ? value : defaultValue;
  }
}
