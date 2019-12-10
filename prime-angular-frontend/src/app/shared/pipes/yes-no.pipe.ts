import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'yesNo'
})
export class YesNoPipe implements PipeTransform {
  transform(value: any): string {
    // Null check to allow for default pipe chaining
    return (value === null)
      ? '' : (value)
        ? 'Yes' : 'No';
  }
}
