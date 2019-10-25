import { FormatDatePipe } from './format-date.pipe';

describe('DatePipe', () => {
  it('create an instance', () => {
    const pipe = new FormatDatePipe();
    expect(pipe).toBeTruthy();
  });
});
