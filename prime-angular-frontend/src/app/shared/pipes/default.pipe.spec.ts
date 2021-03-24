import { DefaultPipe } from './default.pipe';

describe('DefaultPipe', () => {
  let pipe: DefaultPipe;
  beforeEach(() => pipe = new DefaultPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should use a default of "-" when the value is falsey', () => {
    const results = [
      pipe.transform(0),
      pipe.transform(''),
      pipe.transform(null),
      pipe.transform(false),
      pipe.transform(undefined)
    ];
    results.forEach(result => expect(result).toBe('-'));
  });

  it('should use a provided default when the value is falsey', () => {
    const providedDefault = 'N/A';
    const results = [
      pipe.transform(0, providedDefault),
      pipe.transform('', providedDefault),
      pipe.transform(null, providedDefault),
      pipe.transform(false, providedDefault),
      pipe.transform(undefined, providedDefault)
    ];
    results.forEach(result => expect(result).toBe(providedDefault));
  });

  it('should use the value if truthy and not a default value', () => {
    const value = 'Lorem ipsum dolor sit';
    const result = pipe.transform(value);
    expect(result).toBe(value);
  });
});
