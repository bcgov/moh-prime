import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'default'
})
export class DefaultPipe implements PipeTransform {
  public transform(value: any, defaultValue: string = '-'): any {
    return (value) ? value : defaultValue;
  }
}
