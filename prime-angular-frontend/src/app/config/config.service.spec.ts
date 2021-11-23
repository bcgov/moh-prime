import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from './config.service';

describe('ConfigService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule,
      MatSnackBarModule
    ],
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should create', () => {
    const service: ConfigService = TestBed.inject(ConfigService);
    expect(service).toBeTruthy();
  });
});
