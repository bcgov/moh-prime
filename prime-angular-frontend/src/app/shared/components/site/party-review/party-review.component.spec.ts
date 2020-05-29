import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockSiteRegistrationService } from 'test/mocks/mock-site-registration.service';

import { PartyReviewComponent } from './party-review.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SharedModule } from '@shared/shared.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';

describe('PartyReviewComponent', () => {
  let component: PartyReviewComponent;
  let fixture: ComponentFixture<PartyReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SiteRegistrationModule,
        SharedModule
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
        }
      ]
    }).compileComponents();
  }));

  beforeEach(inject([SiteRegistrationService], (siteRegistrationService: SiteRegistrationService) => {
    fixture = TestBed.createComponent(PartyReviewComponent);
    component = fixture.componentInstance;
    component.party = siteRegistrationService.site.location.organization.signingAuthority;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
