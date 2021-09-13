import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockSiteService } from 'test/mocks/mock-site.service';

import { ContactProfileFormComponent } from './contact-profile-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';

describe('ContactProfileFormComponent', () => {
  let component: ContactProfileFormComponent;
  let fixture: ComponentFixture<ContactProfileFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [
        ContactProfileFormComponent
      ],
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule,
        ReactiveFormsModule
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
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject(
    [SiteService, SiteFormStateService],
    (siteService: SiteService, siteFormStateService: SiteFormStateService) => {
      fixture = TestBed.createComponent(ContactProfileFormComponent);
      component = fixture.componentInstance;
      siteFormStateService.setForm(siteService.site);
      // Add the bound FormGroup to the component
      component.form = siteFormStateService.administratorPharmaNetFormState.form;
      fixture.detectChanges();
    })
  );

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
