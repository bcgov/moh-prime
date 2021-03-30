import { StringUtils } from './string-utils.class';

describe('StringUtils', () => {
  const util = StringUtils;

  describe('capitalize', () => {
    it('should capitalize a word', () => {
      const result = util.capitalize('lorem');
      expect(result).toBe('Lorem');
    });

    it('should capitalize only the first word in a sentence', () => {
      const result = util.capitalize('lorem ipsum dolor sit');
      expect(result).toBe('Lorem ipsum dolor sit');
    });

    it('should not fail when passed an empty string', () => {
      const value = '';
      const result = util.capitalize(value);
      expect(result).toBe(value);
    });

    it('should not fail when passed a null', () => {
      const result = util.capitalize(null);
      expect(result).toBeNull();
    });

    it('should not fail when passed undefined', () => {
      const result = util.capitalize(undefined);
      expect(result).toBeUndefined();
    });
  });

  describe('splice', () => {
    const strValue = 'The quick brown fox jumps over the lazy dog.';

    it('should insert text at a specific position in the string', () => {
      const result = util.splice(strValue, 40, 'spotted ');
      expect(result).toBe('The quick brown fox jumps over the lazy spotted dog.');
    });

    it('should insert and remove text at a specific position in the string from the start', () => {
      const result = util.splice(strValue, 35, 'fire engine', 8);
      expect(result).toBe('The quick brown fox jumps over the fire engine.');
    });

    it('should insert and remove text at a specific position in the string from the end', () => {
      const result = util.splice(strValue, -9, 'fire engine', 8);
      expect(result).toBe('The quick brown fox jumps over the fire engine.');
    });

    it('should not fail when passed null', () => {
      const result = util.splice(null, 35, 'fire engine', 8);
      expect(result).toBeNull();
    });

    it('should append to the end of the string when insert position is greater than the text length', () => {
      const result = util.splice(strValue, strValue.length + 10, '.. and then what?');
      expect(result).toBe(`${strValue}.. and then what?`);
    });
  });
});
