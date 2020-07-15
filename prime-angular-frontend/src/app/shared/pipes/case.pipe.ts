import { Pipe, PipeTransform } from '@angular/core';

export type CaseType = 'snake' | 'kebab' | 'camel' | 'pascal' | 'space';

@Pipe({
  name: 'case'
})
// TODO small use case so adding conversions only as needed
export class CasePipe implements PipeTransform {
  public transform(value: string, fromCase: CaseType, toCase: CaseType): string {
    switch (fromCase) {
      case 'space':
        return this.convertSpace(value, toCase);
      default:
        return value;
    }
  }

  private convertSpace(value: string, toCase: CaseType) {
    switch (toCase) {
      case 'kebab':
        return value.replace(/\s/g, '-').toLowerCase();
      default:
        return value;
    }
  }
}
