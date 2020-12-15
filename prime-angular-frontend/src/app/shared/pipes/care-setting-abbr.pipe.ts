import { Pipe, PipeTransform } from '@angular/core';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Pipe({
  name: 'careSettingAbbr'
})
export class CareSettingAbbrPipe implements PipeTransform {

  transform(value: CareSetting | CareSetting[]): string {
    if (Array.isArray(value)) {
      return value.map(item => this.getAbbr(item.careSettingCode)).join(', ');
    }
    return this.getAbbr(<CareSettingEnum>value.careSettingCode);
  }

  private getAbbr(careSetting: CareSettingEnum) {
    switch (careSetting) {
      case CareSettingEnum.COMMUNITY_PHARMACIST:
        return 'CP';
      case CareSettingEnum.DEVICE_PROVIDER:
        return 'DP';
      case CareSettingEnum.HEALTH_AUTHORITY:
        return 'HA';
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
        return 'PCHP';
      default:
        return '-';
    }
  }

}
