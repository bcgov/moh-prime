import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockCommunitySiteService } from 'test/mocks/mock-community-site.service';

import { ContactProfileFormComponent } from './contact-profile-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('ContactProfileFormComponent', () => {
  let component: ContactProfileFormComponent;
  let fixture: ComponentFixture<ContactProfileFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        ContactProfileFormComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        MatSnackBarModule,
        ReactiveFormsModule,
        NgxMaskDirective,
        NgxMaskPipe],
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
            useClass: MockCommunitySiteService
        },
        SiteFormStateService,
        provideNgxMask(),
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
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
