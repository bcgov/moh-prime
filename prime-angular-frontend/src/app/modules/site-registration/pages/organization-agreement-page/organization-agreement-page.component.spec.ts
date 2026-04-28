import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { OrganizationAgreementPageComponent } from './organization-agreement-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SafePipe } from '@shared/pipes/safe.pipe';
import { KeycloakService } from 'keycloak-angular';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { MockOrganizationService } from 'test/mocks/mock-organization.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrganizationAgreementPageComponent', () => {
  let component: OrganizationAgreementPageComponent;
  let fixture: ComponentFixture<OrganizationAgreementPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        OrganizationAgreementPageComponent,
        SafePipe
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: OrganizationService,
            useClass: MockOrganizationService
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationAgreementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
