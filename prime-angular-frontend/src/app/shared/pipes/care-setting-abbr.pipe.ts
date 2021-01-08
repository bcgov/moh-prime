import { Pipe, PipeTransform } from '@angular/core';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Pipe({
  name: 'careSettingAbbr'
})
export class CareSettingAbbrPipe implements PipeTransform {
  public transform(value: CareSettingEnum | CareSettingEnum[]): string | string[] {
    if (Array.isArray(value)) {
      return value.map(item => CareSettingEnum.abbr(item));
    }
    return CareSettingEnum.abbr(value as CareSettingEnum);
  }
}
