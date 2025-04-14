import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, inject, TestBed, waitForAsync } from '@angular/core/testing';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { BusinessLicenceExpiryComponent } from './business-licence-expiry.component';

describe('BusinessLicenceExpiryComponent', () => {
  let component: BusinessLicenceExpiryComponent;
  let fixture: ComponentFixture<BusinessLicenceExpiryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaterialModule,
        BrowserAnimationsModule,
        MatInputModule,
        ReactiveFormsModule,
        MatDatepickerModule,
        NgxMaskDirective,
        NgxMaskPipe
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: PermissionService,
          useClass: MockPermissionService
        },
        provideNgxMask()
      ],
      declarations: [BusinessLicenceExpiryComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(inject([SiteFormStateService], (siteFormStateService: SiteFormStateService) => {
    fixture = TestBed.createComponent(BusinessLicenceExpiryComponent);
    component = fixture.componentInstance;
    component.form = siteFormStateService.businessLicenceFormState.form;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
