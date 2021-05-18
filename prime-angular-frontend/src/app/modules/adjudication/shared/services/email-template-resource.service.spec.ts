import { TestBed } from '@angular/core/testing';

import { EmailTemplateResourceService } from './email-template-resource.service';

describe('EmailTemplateResourceService', () => {
  let service: EmailTemplateResourceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmailTemplateResourceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
