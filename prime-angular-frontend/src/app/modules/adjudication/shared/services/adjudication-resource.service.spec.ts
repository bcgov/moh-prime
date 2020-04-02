import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AdjudicationResource } from './adjudication-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('AdjudicationResource', () => {
  beforeEach(() => TestBed.configureTestingModule({
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
  }));

  it('should create', () => {
    const service: AdjudicationResource = TestBed.inject(AdjudicationResource);
    expect(service).toBeTruthy();
  });
});
