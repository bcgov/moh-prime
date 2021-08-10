import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { BannerMaintenanceComponent } from './banner-maintenance.component';

import { ConfigService } from '@config/config.service';

import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';

import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { SharedModule } from '@shared/shared.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('BannerMaintenanceComponent', () => {
  let component: BannerMaintenanceComponent;
  let fixture: ComponentFixture<BannerMaintenanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        RouterTestingModule,
        MatDialogModule,
        MatSnackBarModule,
        NgxMaterialModule,
        BrowserAnimationsModule,
        // NgxBusyModule,
        // NgxContextualHelpModule,
        // SharedModule,
        // AdjudicationModule
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useValue: MockConfigService
        },
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
    fixture = TestBed.createComponent(BannerMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
