import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'postal'
})
export class PostalPipe implements PipeTransform {
  transform(value: string): string {
    return `${value.toUpperCase().slice(0, 3)} ${value.toUpperCase().slice(3)}`;
  }
}
