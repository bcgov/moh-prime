import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'fullname'
})
export class FullnamePipe implements PipeTransform {
  public transform(model: { firstName: string, lastName: string, [key: string]: any }): string {
    const firstName = model?.firstName;
    const lastName = model?.lastName;
    return (firstName && lastName)
      ? `${firstName} ${lastName}`
      : '';
  }
}
