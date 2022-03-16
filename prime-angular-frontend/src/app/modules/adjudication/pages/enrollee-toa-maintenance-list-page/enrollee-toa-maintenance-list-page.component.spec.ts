import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolleeToaMaintenanceListPageComponent } from './enrollee-toa-maintenance-list-page.component';
import faker from 'faker';

fdescribe('EnrolleeToaMaintenanceListPageComponent', () => {
  let component: EnrolleeToaMaintenanceListPageComponent;
  let fixture: ComponentFixture<EnrolleeToaMaintenanceListPageComponent>;

  let spyOnRouteRelativeTo;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaMaintenanceListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    spyOnRouteRelativeTo = spyOn((component as any).routeUtils, 'routeRelativeTo');
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onBack()', () => {
    it('should call routeRelativeTo with [\'../\']', () => {

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(['../']);
    });
  });

  describe('testing onView()', () => {
    it('should call routeRelativeTo with the passed in Id', () => {
      const mockId = faker.random.number();

      component.onView(mockId);
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith([mockId]);
    });
  });

  describe('testing getToaCardProperties()', () => {
    it('should return a key/value pair of ', () => {
      const mockId = faker.random.number();

      component.onView(mockId);
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith([mockId]);
    });
  });
});
