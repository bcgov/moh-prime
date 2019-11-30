
import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { PostalPipe } from './postal.pipe';

describe('PostalPipe', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
  }));


  it('create an instance', () => {
    const pipe = new PostalPipe();
    expect(pipe).toBeTruthy();
  });
});
