
import { TestBed, async } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolmentPipe } from './enrolment.pipe';

describe('EnrolmentPipe', () => {
  beforeEach(async(() => {
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
