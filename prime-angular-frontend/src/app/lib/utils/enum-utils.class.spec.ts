import { EnumUtils } from '@lib/utils/enum-utils.class';

enum NumericEnum {
  FOGHORN_LEGHORN = 1,
  DONALD_DUCK = 2
}

describe('EnumUtils', () => {
  const util = EnumUtils;

  describe('values', () => {
    it('should provide a list of number values for a numeric enum', () => {
      const result = util.values(NumericEnum);
      expect(result).toEqual([NumericEnum.FOGHORN_LEGHORN, NumericEnum.DONALD_DUCK]);
    });

    it('should not fail when passed null', () => {
      const result = util.values(null);
      expect(result).toEqual([]);
    });
  });

  describe('asObjects', () => {
    it('should provide an object literal with key/value pairs matching an enum', () => {
      const result = util.asObjects(NumericEnum);
      expect(result.length).toBe(2);
      expect(result).toContain(jasmine.objectContaining({
        key: NumericEnum.FOGHORN_LEGHORN,
        value: NumericEnum[NumericEnum.FOGHORN_LEGHORN]
      }));
      expect(result).toContain(jasmine.objectContaining({
        key: NumericEnum.DONALD_DUCK,
        value: NumericEnum[NumericEnum.DONALD_DUCK]
      }));
    });

    it('should not fail when passed null', () => {
      const result = util.values(null);
      expect(result).toEqual([]);
    });
  });
});
