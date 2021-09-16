import { TestBed } from '@angular/core/testing';

import { ElectronicAgreementGuard } from './electronic-agreement.guard';

describe('ElectronicAgreementGuard', () => {
  let guard: ElectronicAgreementGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(ElectronicAgreementGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
