import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { LicenseClassesMaintenancePageComponent } from './license-classes-maintenance-page.component';

describe('LicenseClassesMaintenancePageComponent', () => {
  let component: LicenseClassesMaintenancePageComponent;
  let fixture: ComponentFixture<LicenseClassesMaintenancePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaterialModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseClassesMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  describe('testing onBack()', () => {
    it('should call routeRelativeTo with [\'./\']', () => {
      const spyOnRouteRelativeTo = spyOn((component as any).routeUtils, 'routeRelativeTo');

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(['./']);
    });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
