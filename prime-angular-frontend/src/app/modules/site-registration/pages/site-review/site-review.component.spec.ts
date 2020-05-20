import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockSiteRegistrationService } from 'test/mocks/mock-site-registration.service';

import { SiteReviewComponent } from './site-review.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';

describe('SiteReviewComponent', () => {
  let component: SiteReviewComponent;
  let fixture: ComponentFixture<SiteReviewComponent>;

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
          provide: SiteRegistrationService,
          useClass: MockSiteRegistrationService
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        }
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
