import { JoinPipe } from './join.pipe';

describe('JoinPipe', () => {
  let pipe: JoinPipe;
  beforeEach(() => pipe = new JoinPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should join an array of strings on a comma to produce a comma separated string', () => {
    const result = pipe.transform(['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']);
    expect(result).toBe('Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday');
  });

  it('should not fail when not passed an array', () => {
    const result = pipe.transform('Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday');
    expect(result).toBe('Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday');
  });
});
