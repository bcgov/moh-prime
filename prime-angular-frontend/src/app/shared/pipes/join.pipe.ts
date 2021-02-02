import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'join'
})
export class JoinPipe implements PipeTransform {
  public transform(value: string | string[]): string {
    return (Array.isArray(value))
      ? value.join(', ')
      : value;
  }
}
