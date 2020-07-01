import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { SiteOverviewComponent } from './site-overview.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

describe('SiteOverviewComponent', () => {
  let component: SiteOverviewComponent;
  let fixture: ComponentFixture<SiteOverviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        SiteRegistrationModule
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
          provide: AuthenticationService,
          useClass: MockAuthenticationService
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
