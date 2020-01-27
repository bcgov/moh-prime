import { TestBed, async, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { ConfigResolver } from './config-resolver';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ConfigResolver', () => {
  beforeEach(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule
        ],
        providers: [
          ConfigResolver,
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    );
  });

  it('should create', inject([ConfigResolver], (guard: ConfigResolver) => {
    expect(guard).toBeTruthy();
  }));
});
