import { TestBed } from '@angular/core/testing';

import { PendingTransferGuard } from './pending-transfer.guard';

describe('PendingTransferGuard', () => {
  let guard: PendingTransferGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PendingTransferGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
