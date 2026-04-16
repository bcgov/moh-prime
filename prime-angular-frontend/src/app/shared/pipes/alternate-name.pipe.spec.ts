import { AlternateNamePipe } from './alternate-name.pipe';

describe('AlternateNamePipe', () => {
  let pipe: AlternateNamePipe;
  beforeEach(() => pipe = new AlternateNamePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should remain the same', () => {
    const results = [
      pipe.transform('John.Smith'),
      pipe.transform('John-Smith'),
      pipe.transform('John Smith'),
      pipe.transform('John\'Smith')
    ];
    results.forEach(result => expect(result).toBe(result));
  });

  it('should be double quoted', () => {
    const results = [
      pipe.transform('John.'),
      pipe.transform('John-'),
      pipe.transform('John '),
      pipe.transform('John\''),
      pipe.transform('.Smith'),
      pipe.transform('-Smith'),
      pipe.transform(' Smith'),
      pipe.transform('\'Smith')
    ];
    results.forEach(result => expect(result).toContain('\"'));
  });
});
