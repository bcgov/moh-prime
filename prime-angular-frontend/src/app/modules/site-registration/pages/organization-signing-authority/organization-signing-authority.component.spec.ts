import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { OrganizationSigningAuthorityComponent } from './organization-signing-authority.component';
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

describe('OrganizationSigningAuthorityComponent', () => {
  let component: OrganizationSigningAuthorityComponent;
  let fixture: ComponentFixture<OrganizationSigningAuthorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        OrganizationSigningAuthorityComponent,
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
        NgxMaterialModule
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
        OrganizationFormStateService
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject(
    [OrganizationService, OrganizationFormStateService],
    (organizationService: OrganizationService, organizationFormStateService: OrganizationFormStateService) => {
      fixture = TestBed.createComponent(OrganizationSigningAuthorityComponent);
      component = fixture.componentInstance;
      organizationFormStateService.setForm(organizationService.organization);
      // Add the bound FormGroup to the component
      component.form = organizationFormStateService.signingAuthorityForm as FormGroup;
      fixture.detectChanges();
    })
  );

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
