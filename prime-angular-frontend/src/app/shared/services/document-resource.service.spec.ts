import { TestBed } from '@angular/core/testing';

import { DocumentResource } from './document-resource.service';

describe('DocumentResource', () => {
  let service: DocumentResource;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DocumentResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
