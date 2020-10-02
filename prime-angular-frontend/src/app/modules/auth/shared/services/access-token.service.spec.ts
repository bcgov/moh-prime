import { TestBed } from '@angular/core/testing';
import { AccessTokenService } from './access-token.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';

describe('AccessTokenService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: AccessTokenService,
        useClass: MockAccessTokenService
      }
    ]
  }));

  it('should be created', () => {
    const service: AccessTokenService = TestBed.inject(AccessTokenService);
    expect(service).toBeTruthy();
  });
});
