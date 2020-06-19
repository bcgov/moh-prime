import { Pipe, PipeTransform } from '@angular/core';
import { StringUtils } from '@lib/utils/string-utils.class';

@Pipe({
  name: 'capitalize'
})
export class CapitalizePipe implements PipeTransform {
  transform(value: string, all: boolean = false): string {
    if (value) {
      return (all)
        ? value.split(' ').map((word: string) => StringUtils.capitalize(word)).join(' ')
        : StringUtils.capitalize(value);
    }

    return value;
  }
}
