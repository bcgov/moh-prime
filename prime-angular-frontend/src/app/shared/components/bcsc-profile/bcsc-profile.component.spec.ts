import { SharedModule } from '@shared/shared.module';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { BcscProfileComponent } from './bcsc-profile.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('BcscProfileComponent', () => {
  let component: BcscProfileComponent;
  let fixture: ComponentFixture<BcscProfileComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        BcscProfileComponent,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [ReactiveFormsModule,
        RouterTestingModule,
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
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BcscProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
