import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { GisEnrolmentResource } from './gis-enrolment-resource.service';

xdescribe('GisEnrolmentResource', () => {
  let service: GisEnrolmentResource;

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
    service = TestBed.inject(GisEnrolmentResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
