import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'firstKey'
})
export class FirstKeyPipe implements PipeTransform {
  transform(obj: { [key: string]: string }): any {
    return Object.keys(obj).shift();
  }
}
