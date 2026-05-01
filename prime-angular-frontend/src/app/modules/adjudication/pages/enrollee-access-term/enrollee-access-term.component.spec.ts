import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { EnrolleeAccessTermComponent } from './enrollee-access-term.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EnrolleeAccessTermComponent', () => {
  let component: EnrolleeAccessTermComponent;
  let fixture: ComponentFixture<EnrolleeAccessTermComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        AdjudicationModule],
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
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAccessTermComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
