import { TestBed, inject } from '@angular/core/testing';

import { ApiResource } from './api-resource.service';

describe('ApiResource', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApiResource]
    });
  });

  it('should be created', inject([ApiResource], (service: ApiResource) => {
    expect(service).toBeTruthy();
  }));
});
