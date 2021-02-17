
import { PostalPipe } from './postal.pipe';

describe('PostalPipe', () => {
  let pipe: PostalPipe;
  beforeEach(() => pipe = new PostalPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should format a postal code', () => {
    const result = pipe.transform('m4e2b6');
    expect(result).toBe('M4E 2B6');
  });

  it('should not fail when passed a falsey value', () => {
    const results = [
      pipe.transform(''),
      pipe.transform(null),
      pipe.transform(undefined)
    ];
    results.forEach(result => expect(result).toBe(''));
  });
});
