import { TestBed } from '@angular/core/testing';

import { ApiResourceUtilsService } from './api-resource-utils.service';

describe('ApiResourceUtilsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApiResourceUtilsService = TestBed.inject(ApiResourceUtilsService);
    expect(service).toBeTruthy();
  });
});
