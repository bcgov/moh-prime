import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { PhsaLabtechResource } from './phsa-labtech-resource.service';

describe('PhsaLabtechResource', () => {
  let service: PhsaLabtechResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(PhsaLabtechResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
