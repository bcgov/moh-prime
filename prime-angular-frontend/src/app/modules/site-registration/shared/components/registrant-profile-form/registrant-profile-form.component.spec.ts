import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { RegistrantProfileFormComponent } from './registrant-profile-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { MockSiteService } from 'test/mocks/mock-site.service';

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
          provide: SiteService,
          useClass: MockSiteService
        },
        SiteFormStateService
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(inject(
    [SiteService, SiteFormStateService],
    (siteService: SiteService, siteFormStateService: SiteFormStateService
    ) => {
      fixture = TestBed.createComponent(RegistrantProfileFormComponent);
      component = fixture.componentInstance;
      siteFormStateService.setForm(siteService.site);
      // Add the bound FormGroup to the component
      component.form = siteFormStateService.administratorPharmaNetForm as FormGroup;
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
