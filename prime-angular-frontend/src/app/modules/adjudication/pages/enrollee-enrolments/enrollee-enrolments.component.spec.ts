import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { EnrolleeEnrolmentsComponent } from './enrollee-enrolments.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import faker from 'faker';
import { MatSelectChange } from '@angular/material/select';

describe('EnrolleeEnrolmentsComponent', () => {
  let component: EnrolleeEnrolmentsComponent;
  let fixture: ComponentFixture<EnrolleeEnrolmentsComponent>;

  let spyOnGetAccessTerms;
  let spyOnSetQueryParams;
  let mockEnrollee;

  let mockSnapshotParamId = faker.random.number();
  let mockSelectedYear = faker.random.number();

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        AdjudicationModule,
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeEnrolmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    spyOnGetAccessTerms = spyOn((component as any), 'getAccessTerms');
    spyOnSetQueryParams = spyOn((component as any), 'setQueryParams');
    mockEnrollee = { currentStatusCode: '', previousStatus: { statusCode: '' } };
    mockSnapshotParamId = faker.random.number();
    mockSelectedYear = faker.random.number();
    (component as any).route.snapshot.params.id = mockSnapshotParamId;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing isUnderAdjudication()', () => {
    describe('with currentStatusCode set to Under Review', () => {
      it('should return true', () => {
        mockEnrollee.currentStatusCode = EnrolmentStatusEnum.UNDER_REVIEW;

        expect(component.isUnderAdjudication(mockEnrollee)).toBeTrue();
      });
    });

    describe('with currentStatusCode set to Requires TOA', () => {
      it('should return true', () => {
        mockEnrollee.currentStatusCode = EnrolmentStatusEnum.REQUIRES_TOA;

        expect(component.isUnderAdjudication(mockEnrollee)).toBeTrue();
      });
    });

    describe('with currentStatusCode set to Editable and Previous status set to Under Review', () => {
      it('should return true', () => {
        mockEnrollee.currentStatusCode = EnrolmentStatusEnum.EDITABLE;
        mockEnrollee.previousStatus.statusCode = EnrolmentStatusEnum.UNDER_REVIEW;

        expect(component.isUnderAdjudication(mockEnrollee)).toBeTrue();
      });
    });

    describe('with currentStatusCode set to Editable and Previous status set to anything but Under Review', () => {
      it('should return false', () => {
        mockEnrollee.currentStatusCode = EnrolmentStatusEnum.EDITABLE;

        expect(component.isUnderAdjudication(mockEnrollee)).toBeFalse();
      });
    });
  });

  describe('testing onAction()', () => {
    it('should call getAccessTerms with the passed in parameters of route.snapshot.params.id and selected year', () => {
      component.selectedYear = mockSelectedYear;

      component.onAction();
      expect(spyOnGetAccessTerms).toHaveBeenCalledOnceWith(mockSnapshotParamId, mockSelectedYear);
    });
  });

  describe('testing onChange()', () => {
    it('should call setQAueryParams with year param and getAccessTerms with route.snapshot.id and year params', () => {
      const mockYear = faker.random.number();

      component.onChange({ value: mockYear } as MatSelectChange);
      expect(spyOnSetQueryParams).toHaveBeenCalledOnceWith({ year: mockYear });
      expect(spyOnGetAccessTerms).toHaveBeenCalledOnceWith(mockSnapshotParamId, mockYear);
    });
  });
});
