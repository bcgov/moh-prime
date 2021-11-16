import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { ApiResource } from './api-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('ApiResource', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        ApiResource,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
  });

  it('should be created', inject([ApiResource], (service: ApiResource) => {
    expect(service).toBeTruthy();
  }));
});
