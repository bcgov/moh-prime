import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { AdjudicationResource } from './adjudication-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AdjudicationResource', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}));

  it('should create', () => {
    const service: AdjudicationResource = TestBed.inject(AdjudicationResource);
    expect(service).toBeTruthy();
  });
});
