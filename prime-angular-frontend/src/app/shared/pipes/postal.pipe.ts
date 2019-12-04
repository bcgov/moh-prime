import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'postal'
})
export class PostalPipe implements PipeTransform {
  public transform(value: string): string {
    return (value) ? this.postalValue(value) : '';
  }

  private postalValue(value: string) {
    return `${value.toUpperCase().slice(0, 3)} ${value.toUpperCase().slice(3)}`;
  }
}
