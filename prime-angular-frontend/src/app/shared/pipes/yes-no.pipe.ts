import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'yesNo'
})
export class YesNoPipe implements PipeTransform {
  transform(value: any, explicit: boolean = false): string {
    // Null check to allow for default pipe chaining, but allow
    // for an explicit yes or no if required
    return (value === null && !explicit)
      ? '' : (value)
        ? 'Yes' : 'No';
  }
}
