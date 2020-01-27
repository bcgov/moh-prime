import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { DataResource } from './data-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('DataResource', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      HttpClientTestingModule
    ],
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should be created', () => {
    const service: DataResource = TestBed.get(DataResource);
    expect(service).toBeTruthy();
  });
});
