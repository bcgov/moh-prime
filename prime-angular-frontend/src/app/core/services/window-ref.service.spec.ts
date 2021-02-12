import { TestBed } from '@angular/core/testing';
import { KeycloakService } from 'keycloak-angular';

import { WindowRefService } from './window-ref.service';

describe('WindowRefService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      KeycloakService
    ]
  }));

  it('should create', () => {
    const service: WindowRefService = TestBed.inject(WindowRefService);
    expect(service).toBeTruthy();
  });
});
