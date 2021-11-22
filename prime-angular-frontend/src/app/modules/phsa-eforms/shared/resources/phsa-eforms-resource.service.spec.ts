import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { PhsaEformsResource } from './phsa-eforms-resource.service';

describe('PhsaEformsResource', () => {
  let service: PhsaEformsResource;

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
    service = TestBed.inject(PhsaEformsResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
