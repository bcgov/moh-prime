import { MockPermissionService } from '../../../../../../test/mocks/mock-permission.service';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { TriageComponent } from './triage.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('TriageComponent', () => {
  let component: TriageComponent;
  let fixture: ComponentFixture<TriageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        NgxMaterialModule,
        SharedModule],
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
            provide: PermissionService,
            useClass: MockPermissionService
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TriageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
