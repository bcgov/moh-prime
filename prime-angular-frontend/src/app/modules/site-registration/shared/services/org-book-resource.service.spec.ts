import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { OrgBookResource } from './org-book-resource.service';
import { SharedModule } from '@shared/shared.module';

describe('OrgBookResource', () => {
  let service: OrgBookResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SharedModule
      ]
    });
    service = TestBed.inject(OrgBookResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
