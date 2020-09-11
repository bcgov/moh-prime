import { Pipe, PipeTransform } from '@angular/core';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

export type CaseType = 'snake' | 'kebab' | 'camel' | 'pascal' | 'space';

@Pipe({
  name: 'case'
})
// TODO small use case so simple conversions only as needed
export class CasePipe implements PipeTransform {
  public transform(value: string, fromCase: CaseType, toCase: CaseType): string {
    if (!value) {
      return value;
    }

    switch (fromCase) {
      case 'space':
        return this.fromSpace(value, toCase);
      case 'snake':
        return this.fromSnake(value, toCase);
      default:
        return value;
    }
  }

  private fromSpace(value: string, toCase: CaseType) {
    switch (toCase) {
      case 'kebab':
        return value.replace(/\s/g, '-').toLowerCase();
      default:
        return value;
    }
  }

  private fromSnake(value: string, toCase: CaseType) {
    switch (toCase) {
      case 'space':
        return value.replace(/_/g, ' ').toLowerCase();
      default:
        return value;
    }
  }
}
