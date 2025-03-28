import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { OrganizationSigningAuthorityPageComponent } from './organization-signing-authority-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

describe('OrganizationSigningAuthorityPageComponent', () => {
  let component: OrganizationSigningAuthorityPageComponent;
  let fixture: ComponentFixture<OrganizationSigningAuthorityPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [
        OrganizationSigningAuthorityPageComponent,
        DefaultPipe,
        FullnamePipe,
        FormatDatePipe,
        ConfigCodePipe,
        PostalPipe
      ],
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        NgxMaskDirective,
        NgxMaskPipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        {
          provide: OrganizationService,
          useClass: MockOrganizationService
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        OrganizationFormStateService,
        provideNgxMask()
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject(
    [OrganizationService, OrganizationFormStateService],
    (organizationService: OrganizationService, organizationFormStateService: OrganizationFormStateService) => {
      fixture = TestBed.createComponent(OrganizationSigningAuthorityPageComponent);
      component = fixture.componentInstance;
      organizationFormStateService.setForm(organizationService.organization);
      // Add the bound FormGroup to the component
      component.formState = organizationFormStateService.organizationSigningAuthorityPageFormState;
      fixture.detectChanges();
    })
  );

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
