import { StatusReasonsPipe } from './status-reasons.pipe';

describe('StatusReasonsPipe', () => {
  it('create an instance', () => {
    const pipe = new StatusReasonsPipe();
    expect(pipe).toBeTruthy();
  });
});
