import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { PartyReviewComponent } from './party-review.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SharedModule } from '@shared/shared.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('PartyReviewComponent', () => {
  let component: PartyReviewComponent;
  let fixture: ComponentFixture<PartyReviewComponent>;

  beforeEach(waitForAsync(() => {
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
          provide: OrganizationService,
          useClass: MockOrganizationService
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject([OrganizationService], (organizationService: OrganizationService) => {
    fixture = TestBed.createComponent(PartyReviewComponent);
    component = fixture.componentInstance;
    component.party = organizationService.organization.signingAuthority;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
