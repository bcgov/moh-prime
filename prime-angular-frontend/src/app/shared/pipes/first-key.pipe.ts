import { Pipe, PipeTransform } from '@angular/core';

// TODO: temporary pipe for form validation messages, but will be
// replaced with a custom form component with errors so forms
// are DRY
@Pipe({
  name: 'firstKey'
})
export class FirstKeyPipe implements PipeTransform {
  transform(obj: { [key: string]: string }): any {
    return Object.keys(obj).shift();
  }
}
