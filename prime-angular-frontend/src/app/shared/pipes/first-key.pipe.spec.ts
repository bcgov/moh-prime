import { FirstKeyPipe } from './first-key.pipe';

describe('FirstKeyPipe', () => {
  it('create an instance', () => {
    const pipe = new FirstKeyPipe();
    expect(pipe).toBeTruthy();
  });
});
