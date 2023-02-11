import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fullname'
})
export class FullnamePipe implements PipeTransform {
  public transform(model: { firstName: string, lastName: string, [key: string]: any }): string {
    if (!model) {
      return null;
    }

    const { firstName, lastName } = model;
    return (lastName)
      ? (firstName) ? `${firstName} ${lastName}`
        : lastName
      : '';
  }
}
