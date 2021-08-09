import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { ConfigService } from '@config/config.service';

import { AdjudicationModule } from '@adjudication/adjudication.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { EnrolleeToaMaintenanceListPageComponent } from './enrollee-toa-maintenance-list-page.component';

describe('EnrolleeToaMaintenanceListPageComponent', () => {
  let component: EnrolleeToaMaintenanceListPageComponent;
  let fixture: ComponentFixture<EnrolleeToaMaintenanceListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        // BrowserAnimationsModule,
        RouterTestingModule,
        // HttpClientTestingModule,
        // NgxBusyModule,
        // NgxContextualHelpModule,
        // NgxMaterialModule,
        // ReactiveFormsModule,
        // RouterTestingModule,
        // AdjudicationModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        // {
        //   provide: ConfigService,
        //   useValue: MockConfigService
        // },
        // {
        //   provide: AuthService,
        //   useClass: MockAuthService
        // },
        // {
        //   provide: PermissionService,
        //   useClass: MockPermissionService
        // }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaMaintenanceListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
