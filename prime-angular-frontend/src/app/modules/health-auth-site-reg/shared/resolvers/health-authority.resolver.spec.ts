import { TestBed } from '@angular/core/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthorityResolver } from './health-authority.resolver';

describe('HealthAuthorityResolver', () => {
  let resolver: HealthAuthorityResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: AppConfig
        },
        CapitalizePipe
      ]
    });
    resolver = TestBed.inject(HealthAuthorityResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
