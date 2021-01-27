import { PhonePipe } from './phone.pipe';

describe('PhonePipe', () => {
  let pipe: PhonePipe;
  beforeEach(() => pipe = new PhonePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should format a 10-digit number as a phone number', () => {
    const result = pipe.transform('9876543210');
    expect(result).toBe('(987) 654-3210');
  });

  it('should format a phone number that contains 10-digits after removing extraneous spaces', () => {
    const result = pipe.transform(' 987 654 3210 ');
    expect(result).toBe('(987) 654-3210');
  });

  it('should not format a phone number that is greater than 10-digits', () => {
    const result = pipe.transform('98765432109');
    expect(result).toBe('(987) 654-3210');
  });

  it('should not format a phone number that is less than 10-digits', () => {
    const result = pipe.transform('987654321');
    expect(result).toBe('(987) 654-3210');
  });

  it('should not format a phone number that is alpha-numeric', () => {
    const result = pipe.transform('987654321a');
    expect(result).toBe('(987) 654-3210');
  });

  it('should not format a phone number that is null', () => {
    const result = pipe.transform(null);
    expect(result).toBeNull();
  });
});
