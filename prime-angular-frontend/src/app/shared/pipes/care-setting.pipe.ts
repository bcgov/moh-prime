import { Pipe, PipeTransform } from '@angular/core';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Pipe({
  name: 'careSetting'
})
export class CareSettingPipe implements PipeTransform {
  public transform(value: CareSettingEnum | CareSettingEnum[], format: 'name' | 'abbr' = 'name'): string | string[] {
    if (!value) {
      return '';
    }

    return (Array.isArray(value))
      ? value.map(cs => this.format(format, cs))
      : this.format(format, value);
  }

  private format(format: 'name' | 'abbr', careSetting: CareSettingEnum): string {
    return (format === 'abbr')
      ? this.getCareSettingAbbreviation(careSetting)
      : this.getCareSettingName(careSetting);
  }

  private getCareSettingAbbreviation(careSetting: CareSettingEnum): string {
    switch (careSetting) {
      case CareSettingEnum.COMMUNITY_PHARMACY:
        return 'CP';
      case CareSettingEnum.DEVICE_PROVIDER:
        return 'DP';
      case CareSettingEnum.HEALTH_AUTHORITY:
        return 'HA';
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
        return 'PCHP';
      default:
        // Allow the DefaultPipe to determine a proper default value
        return '';
    }
  }

  private getCareSettingName(careSetting: CareSettingEnum): string {
    switch (careSetting) {
      case CareSettingEnum.COMMUNITY_PHARMACY:
        return 'Community Pharmacist';
      case CareSettingEnum.DEVICE_PROVIDER:
        return 'Device Provider';
      case CareSettingEnum.HEALTH_AUTHORITY:
        return 'Health Authority';
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
        return 'Private Community Health Practice';
      default:
        // Allow the DefaultPipe to determine a proper default value
        return '';
    }
  }
}
