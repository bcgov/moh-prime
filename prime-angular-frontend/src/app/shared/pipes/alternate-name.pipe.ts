import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'alternateName'
})
export class AlternateNamePipe implements PipeTransform {
  public transform(name: any): any {
    if (typeof name === 'string') {
      // add couple quote if ends or starts from one of special characters
      if (name.endsWith(" ", name.length) || name.endsWith("-", name.length) || name.endsWith("'", name.length) || name.endsWith(".", name.length) ||
        name.startsWith(" ", 0) || name.startsWith("-", 0) || name.startsWith("'", 0) || name.startsWith(".", 0)) {
        name = `"${name}"`;
      }
    }
    return name;
  }
}
