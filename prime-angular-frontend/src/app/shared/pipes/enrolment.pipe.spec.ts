
import { TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolmentPipe } from './enrolment.pipe';

describe('EnrolmentPipe', () => {
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
  }));

  it('create an instance of Enrolment Pipe', () => {
    const pipe = new EnrolmentPipe();
    expect(pipe).toBeTruthy();
  });
});
