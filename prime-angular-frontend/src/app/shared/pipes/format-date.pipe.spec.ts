import { FormatDatePipe } from './format-date.pipe';

describe('FormatDatePipe', () => {
  let pipe: FormatDatePipe;
  beforeEach(() => pipe = new FormatDatePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should format the date to the default date format', () => {
    const value = '1977-09-22T14:30:00';
    const result = pipe.transform(value);
    expect(result).toBe('22 Sep 1977');
  });

  it('should format the date to a requested format', () => {
    const value = '1977-09-22T14:30:00';
    const format = 'MMMM DD, YYYY HH:mm a';
    const result = pipe.transform(value, format);
    expect(result).toBe('September 22, 1977 14:30 pm');
  });

  it('should not fail when passed a null', () => {
    const value = null;
    const result = pipe.transform(value);
    expect(result).toBe('');
  });
});
