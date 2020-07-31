import { SafePipe } from './safe.pipe';
import { ɵDomSanitizerImpl } from '@angular/platform-browser';

describe('SafePipe', () => {
  it('create an instance', () => {
    const document = new Document();
    const sanitizer = new ɵDomSanitizerImpl(document);
    const pipe = new SafePipe(sanitizer);
    expect(pipe).toBeTruthy();
  });
});
