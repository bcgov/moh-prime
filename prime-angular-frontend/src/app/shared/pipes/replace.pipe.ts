import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'replace',
    standalone: false
})
export class ReplacePipe implements PipeTransform {
  public transform(value: string, replace: string, replaceWith: string): string {
    return (value)
      ? value.replace(new RegExp(replace, 'g'), replaceWith)
      : value;
  }
}
