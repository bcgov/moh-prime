export enum CareSettingEnum {
  HEALTH_AUTHORITY = 1,
  PRIVATE_COMMUNITY_HEALTH_PRACTICE,
  COMMUNITY_PHARMACIST,
  DEVICE_PROVIDER
}

export namespace CareSettingEnum {
  export function abbr(careSetting: CareSettingEnum): string {
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
