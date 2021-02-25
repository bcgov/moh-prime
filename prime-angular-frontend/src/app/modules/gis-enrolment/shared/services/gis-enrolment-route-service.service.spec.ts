import { TestBed } from '@angular/core/testing';

import { GisEnrolmentRouteServiceService } from './gis-enrolment-route-service.service';

describe('GisEnrolmentRouteServiceService', () => {
  let service: GisEnrolmentRouteServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GisEnrolmentRouteServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
