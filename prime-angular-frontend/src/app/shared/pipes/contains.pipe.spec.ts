import { ContainsPipe } from './contains.pipe';

describe('ContainsPipe', () => {
  let pipe: ContainsPipe;
  const value = 'Hello World';
  let search;
  beforeEach(() => {
    pipe = new ContainsPipe();
    search = '';
  });

  it('create an instance', () => expect(pipe).toBeTruthy());

  describe('testing the value includes search', () => {
    it('should be true for search contained in value', () => {
      let result = pipe.transform(value, 'Hello');
      expect(result).toBeTruthy();

      result = pipe.transform(value, 'World');
      expect(result).toBeTruthy();
    });

    it('should be false for search not contained in value', () => {
      const result = pipe.transform(value, 'Goodbye');
      expect(result).toBeFalsy();
    });
  });

  describe('testing the value startsWith search', () => {
    it('should be true when value startsWith search', () => {
      const result = pipe.transform(value, 'Hello', 'startsWith');
      expect(result).toBeTruthy();
    });

    it('should be false when value does not startsWith search', () => {
      const result = pipe.transform(value, 'World', 'startsWith');
      expect(result).toBeFalsy();
    });
  });

  describe('testing the value endsWith search', () => {
    it('should be true', () => {
      const result = pipe.transform(value, 'World', 'endsWith')
      expect(result).toBeTruthy();
    });

    it('should be false', () => {
      const result = pipe.transform(value, 'Hello', 'endsWith');
      expect(result).toBeFalsy();
    });
  });

  describe('testing with null value and/or search', () => {
    it('should be false for null value', () => {
      const result = pipe.transform(null, search);
      expect(result).toBeFalsy();
    });

    it('should be false for null search', () => {
      const result = pipe.transform(value, null);
      expect(result).toBeFalsy();
    });

    it('should be false for null value and search', () => {
      const result = pipe.transform(null, null);
      expect(result).toBeFalsy();
    });
  });
});
