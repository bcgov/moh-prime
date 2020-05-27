import { TestBed } from '@angular/core/testing';

import { DocumentResourceService } from './document-resource.service';

describe('DocumentResourceService', () => {
  let service: DocumentResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DocumentResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
