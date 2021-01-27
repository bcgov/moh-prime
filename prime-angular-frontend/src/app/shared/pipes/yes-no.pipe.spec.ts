import { YesNoPipe } from './yes-no.pipe';

describe('YesNoPipe', () => {
  let pipe: YesNoPipe;
  beforeEach(() => pipe = new YesNoPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should be "Yes" when the value is truthy', () => {
    const results = [
      pipe.transform(1),
      pipe.transform('lorem ipsum dolor'),
      pipe.transform(true)
    ];
    results.forEach(result => expect(result).toBe('Yes'));
  });

  it('should be "No" when value is falsey, but not null', () => {
    const results = [
      pipe.transform(0),
      pipe.transform(''),
      pipe.transform(false),
      pipe.transform(undefined)
    ];
    results.forEach(result => expect(result).toBe('No'));
  });

  it('should be an empty string when null and no explicity result is requested', () => {
    const result = pipe.transform(null);
    expect(result).toBe('');
  });

  it('should be "No" when null and an explicit result is requested', () => {
    const result = pipe.transform(null, true);
    expect(result).toBe('No');
  });
});
