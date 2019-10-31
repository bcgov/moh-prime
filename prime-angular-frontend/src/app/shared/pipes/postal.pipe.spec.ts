import { PostalPipe } from './postal.pipe';

describe('PostalPipe', () => {
  it('create an instance', () => {
    const pipe = new PostalPipe();
    expect(pipe).toBeTruthy();
  });
});
