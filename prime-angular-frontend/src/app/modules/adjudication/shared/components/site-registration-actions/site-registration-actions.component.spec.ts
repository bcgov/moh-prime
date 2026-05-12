import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { SiteRegistrationActionsComponent } from './site-registration-actions.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { ConfigService } from '@config/config.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteRegistrationActionsComponent', () => {
  let component: SiteRegistrationActionsComponent;
  let fixture: ComponentFixture<SiteRegistrationActionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [SiteRegistrationModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
