
import { TestBed, waitForAsync, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { PostalPipe } from './postal.pipe';

describe('PostalPipe', () => {
  beforeEach(waitForAsync(() => {
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
