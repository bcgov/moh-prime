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

  describe('insertResultIf', () => {
    it('should insert result into array using anonymous method', () => {
      const result = [
        1, 2, 3,
        ...util.insertResultIf(true, () => [4, 5, 6]),
        7, 8, 9
      ];

      expect(result).toEqual([1, 2, 3, 4, 5, 6, 7, 8, 9]);
    });

    it('should insert result into array using named method with parameters', () => {
      const doSomething = (...params) => [...params, 7, 8, 9];
      const result = [
        1, 2, 3,
        ...util.insertResultIf(true, () => doSomething(4, 5, 6)),
        10, 11, 12
      ];

      expect(result).toEqual([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]);
    });

    it('should not insert result into array using anonymous method', () => {
      const result = [
        1, 2, 3,
        ...util.insertResultIf(false, () => [4, 5, 6]),
        7, 8, 9
      ];

      expect(result).toEqual([1, 2, 3, 7, 8, 9]);
    });

    it('should not insert result into array using named method with parameters', () => {
      const doSomething = (...params) => [...params, 7, 8, 9];
      const result = [
        1, 2, 3,
        ...util.insertResultIf(false, () => doSomething(4, 5, 6)),
        10, 11, 12
      ];

      expect(result).toEqual([1, 2, 3, 10, 11, 12]);
    });
  });
});
