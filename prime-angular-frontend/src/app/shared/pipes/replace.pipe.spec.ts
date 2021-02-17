import { ReplacePipe } from './replace.pipe';

describe('ReplacePipe', () => {
  let pipe: ReplacePipe;
  beforeEach(() => pipe = new ReplacePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should replace all occurences of a string within a string', () => {
    const value = 'Have a happy happy happy Christmas!';
    const replace = 'happy';
    const replaceWith = 'merry';
    expect(value).not.toContain(replaceWith);
    const result = pipe.transform(value, replace, replaceWith);
    expect(result).toContain(replaceWith);
    expect(result.split(' ').filter(s => s === replaceWith).length).toBe(3);
    expect(result).toBe('Have a merry merry merry Christmas!');
  });

  it('should not replace anything when no occurences of a string within a string', () => {
    const value = 'Have a merry merry merry Christmas!';
    const replace = 'happy';
    const replaceWith = 'terrible';
    expect(value).not.toContain(replace);
    const result = pipe.transform(value, replace, replaceWith);
    expect(result).not.toContain(replaceWith);
    expect(result).toBe(value);
  });
});
