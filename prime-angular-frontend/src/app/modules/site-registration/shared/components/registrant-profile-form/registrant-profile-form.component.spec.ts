import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockSiteRegistrationService } from 'test/mocks/mock-site-registration.service';

import { RegistrantProfileFormComponent } from './registrant-profile-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

describe('RegistrantProfileFormComponent', () => {
  let component: RegistrantProfileFormComponent;
  let fixture: ComponentFixture<RegistrantProfileFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SiteRegistrationModule,
        BrowserAnimationsModule,
        RouterTestingModule
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
        SiteRegistrationStateService
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(inject(
    [SiteRegistrationService, SiteRegistrationStateService],
    (siteRegistrationService: SiteRegistrationService, siteRegistrationStateService: SiteRegistrationStateService
    ) => {
      fixture = TestBed.createComponent(RegistrantProfileFormComponent);
      component = fixture.componentInstance;
      siteRegistrationStateService.setSite(siteRegistrationService.site);
      // Add the bound FormGroup to the component
      component.form = siteRegistrationStateService.administratorPharmaNetForm as FormGroup;
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
