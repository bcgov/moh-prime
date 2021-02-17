import { CapitalizePipe } from './capitalize.pipe';

describe('CapitalizePipe', () => {
  let pipe: CapitalizePipe;
  beforeEach(() => pipe = new CapitalizePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should capitalize a word', () => {
    const result = pipe.transform('lorem');
    expect(result).toBe('Lorem');
  });

  it('should capitalize only the first word in a sentence', () => {
    const result = pipe.transform('lorem ipsum dolor sit');
    expect(result).toBe('Lorem ipsum dolor sit');
  });

  it('should capitalize all the words in a sentence', () => {
    const result = pipe.transform('lorem ipsum dolor sit', true);
    expect(result).toBe('Lorem Ipsum Dolor Sit');
  });

  it('should not fail when passed an empty string', () => {
    const value = '';
    const result = pipe.transform(value);
    expect(result).toBe(value);
  });

  it('should not fail when passed a null', () => {
    const result = pipe.transform(null);
    expect(result).toBeNull();
  });

  it('should not fail when passed undefined', () => {
    const result = pipe.transform(undefined);
    expect(result).toBeUndefined();
  });
});
