import { ArrayUtils } from '@lib/utils/array-utils.class';

describe('ArrayUtils', () => {
  const util = ArrayUtils;

  describe('insertIf', () => {
    it('should insert element into an array in order when predicate is true', () => {
      const result = [
        'Yosemite Sam',
        ...util.insertIf(true, 'Foghorn Leghorn'),
        'Road Runner'
      ];

      expect(result).toEqual(['Yosemite Sam', 'Foghorn Leghorn', 'Road Runner']);
    });

    it('should insert elements into an array in order when predicate is true', () => {
      const result = [
        'Yosemite Sam',
        ...util.insertIf(true, 'Foghorn Leghorn', 'Donald Duck'),
        'Road Runner'
      ];

      expect(result).toEqual(['Yosemite Sam', 'Foghorn Leghorn', 'Donald Duck', 'Road Runner']);
    });

    it('should insert an array of elements into an array in order when predicate is true', () => {
      const result = [
        'Yosemite Sam',
        ...util.insertIf(true, ['Foghorn Leghorn', 'Donald Duck']),
        'Road Runner'
      ];

      expect(result).toEqual(['Yosemite Sam', 'Foghorn Leghorn', 'Donald Duck', 'Road Runner']);
    });

    it('should not insert an elements into an array in order when predicate is false', () => {
      const result = [
        'Yosemite Sam',
        ...util.insertIf(false, 'Foghorn Leghorn', 'Donald Duck'),
        'Road Runner'
      ];

      expect(result).toEqual(['Yosemite Sam', 'Road Runner']);
    });

    it('should not insert an array of elements into an array in order when predicate is false', () => {
      const result = [
        'Yosemite Sam',
        ...util.insertIf(false, ['Foghorn Leghorn', 'Donald Duck']),
        'Road Runner'
      ];

      expect(result).toEqual(['Yosemite Sam', 'Road Runner']);
    });

    it('should not fail when passed null when predicate is true', () => {
      const result = [
        'Yosemite Sam',
        ...util.insertIf(true, null),
        'Road Runner'
      ];

      expect(result).toEqual(['Yosemite Sam', null, 'Road Runner']);
    });
  });
});
