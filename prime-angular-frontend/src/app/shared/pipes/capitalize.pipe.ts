import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'capitalize'
})
export class CapitalizePipe implements PipeTransform {
  transform(value: string, all: boolean = false): string {
    if (value) {
      return (all)
        ? value.split(' ').map((word: string) => this.capitalizeWord(word)).join(' ')
        : this.capitalizeWord(value);
    }

    return value;
  }

  private capitalizeWord(value: string): string {
    return value.charAt(0).toUpperCase() + value.slice(1).toLowerCase();
  }
}
