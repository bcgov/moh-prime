import faker from 'faker';

import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolmentComponent } from './enrolment.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { KeycloakService } from 'keycloak-angular';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { Address } from '@lib/models/address.model';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

describe('EnrolmentComponent', () => {
  let component: EnrolmentComponent;
  let fixture: ComponentFixture<EnrolmentComponent>;

  let spyOnRouteWithin;
  let mockRoute;
  let mockHttpEnrollee;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          RouterTestingModule,
          AdjudicationModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          KeycloakService
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onRoute()', () => {
    it('with a string only', () => {
      spyOnRouteWithin = spyOn((component as any).routeUtils, 'routeWithin');
      mockRoute = 'mockRoute';
      component.onRoute(mockRoute);

      expect(spyOnRouteWithin).toHaveBeenCalledWith(mockRoute);
    });
  });

  describe('testing enrolleeAdapterResponse()', () => {
    describe('with all addressTypes, certifications, and enrolleeCareSettings as null', () => {
      it('should create empty objects/arrays for all addressTypes, certifications, and enrolleeCareSettings', () => {
        mockHttpEnrollee = new MockEnrolmentService().enrolment;
        mockHttpEnrollee.certifications = null;
        mockHttpEnrollee.enrolleeCareSettings = null;
        mockHttpEnrollee.verifiedAddress = null;
        mockHttpEnrollee.mailingAddress = null;
        mockHttpEnrollee.physicalAddress = null;

        expect(mockHttpEnrollee.certifications).toEqual(null);
        expect(mockHttpEnrollee.enrolleeCareSettings).toEqual(null);
        expect(mockHttpEnrollee.verifiedAddress).toEqual(null);
        expect(mockHttpEnrollee.mailingAddress).toEqual(null);
        expect(mockHttpEnrollee.physicalAddress).toEqual(null);

        component.enrolleeAdapterResponse(mockHttpEnrollee);

        expect(mockHttpEnrollee.certifications).toEqual([]);
        expect(mockHttpEnrollee.enrolleeCareSettings).toEqual([]);
        expect(mockHttpEnrollee.verifiedAddress).toEqual(new Address());
        expect(mockHttpEnrollee.mailingAddress).toEqual(new Address());
        expect(mockHttpEnrollee.physicalAddress).toEqual(new Address());
      });
    });

    describe('with all addressTypes populated and certifications, and enrolleeCareSettings as null', () => {
      it('should create empty arrays for certifications, and enrolleeCareSettings and no change to addresses', () => {
        const mockAddress = new Address('CA', 'BC', faker.address.streetAddress(), '', faker.address.city(), faker.address.zipCode())

        mockHttpEnrollee = new MockEnrolmentService().enrolment;
        mockHttpEnrollee.certifications = null;
        mockHttpEnrollee.enrolleeCareSettings = null;
        mockHttpEnrollee.verifiedAddress = mockAddress;
        mockHttpEnrollee.mailingAddress = mockAddress;
        mockHttpEnrollee.physicalAddress = mockAddress;

        expect(mockHttpEnrollee.certifications).toEqual(null);
        expect(mockHttpEnrollee.enrolleeCareSettings).toEqual(null);
        expect(mockHttpEnrollee.verifiedAddress).toEqual(mockAddress);
        expect(mockHttpEnrollee.mailingAddress).toEqual(mockAddress);
        expect(mockHttpEnrollee.physicalAddress).toEqual(mockAddress);

        component.enrolleeAdapterResponse(mockHttpEnrollee);

        expect(mockHttpEnrollee.certifications).toEqual([]);
        expect(mockHttpEnrollee.enrolleeCareSettings).toEqual([]);
        expect(mockHttpEnrollee.verifiedAddress).toEqual(mockAddress);
        expect(mockHttpEnrollee.mailingAddress).toEqual(mockAddress);
        expect(mockHttpEnrollee.physicalAddress).toEqual(mockAddress);
      });
    });
  });
});
