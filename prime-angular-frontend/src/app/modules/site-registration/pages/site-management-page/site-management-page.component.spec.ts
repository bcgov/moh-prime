import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { AuthService } from '@auth/shared/services/auth.service';
import { SiteManagementPageComponent } from './site-management-page.component';

describe('SiteManagementPageComponent', () => {
  let component: SiteManagementPageComponent;
  let fixture: ComponentFixture<SiteManagementPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      declarations: [
        SiteManagementPageComponent,
        FullnamePipe,
        AddressPipe,
        ConfigCodePipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        KeycloakService,
        FullnamePipe,
        AddressPipe,
        ConfigCodePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
