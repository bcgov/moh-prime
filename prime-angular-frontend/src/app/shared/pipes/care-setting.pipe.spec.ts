import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CareSettingPipe } from './care-setting.pipe';

describe('CareSettingPipe', () => {
  let pipe: CareSettingPipe;
  beforeEach(() => pipe = new CareSettingPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should provide a care setting name for each care setting', () => {
    const value = CareSettingEnum.COMMUNITY_PHARMACY;
    const result = pipe.transform(value, 'name');
    expect(result).toBe('Community Pharmacist');
  });

  it('should provide care setting names for each care setting', () => {
    const value = [
      CareSettingEnum.COMMUNITY_PHARMACY,
      CareSettingEnum.DEVICE_PROVIDER,
      CareSettingEnum.HEALTH_AUTHORITY,
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
    ];
    const results = pipe.transform(value, 'name') as string[];
    const names = [
      'Community Pharmacist',
      'Device Provider',
      'Health Authority',
      'Private Community Health Practice'
    ];
    results.forEach((result, i) => expect(result).toBe(names[i]));
  });

  it('should provide a care setting abbreviation for a care setting', () => {
    const value = CareSettingEnum.COMMUNITY_PHARMACY;
    const result = pipe.transform(value, 'abbr');
    expect(result).toBe('CP');
  });

  it('should provide care setting abbreviations for every care setting', () => {
    const value = [
      CareSettingEnum.COMMUNITY_PHARMACY,
      CareSettingEnum.DEVICE_PROVIDER,
      CareSettingEnum.HEALTH_AUTHORITY,
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
    ];
    const results = pipe.transform(value, 'abbr') as string[];
    const abbrs = [
      'CP',
      'DP',
      'HA',
      'PCHP'
    ];
    results.forEach((result, i) => expect(result).toBe(abbrs[i]));
  });

  it('should not fail when passed an unknown care setting', () => {
    const value = null;
    const result = pipe.transform(value, 'name');
    const name = '';
    expect(result).toBe(name);
  });
});
