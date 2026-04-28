import { ComponentFixture, inject, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { MockCommunitySiteService } from 'test/mocks/mock-community-site.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { BusinessLicenceRenewalPageComponent } from './business-licence-renewal-page.component';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('BusinessLicenceRenewalPageComponent', () => {
  let component: BusinessLicenceRenewalPageComponent;
  let fixture: ComponentFixture<BusinessLicenceRenewalPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        BusinessLicenceRenewalPageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: SiteService,
            useClass: MockCommunitySiteService
        },
        SiteFormStateService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(inject(
    [SiteService, SiteFormStateService],
    (siteService: SiteService, siteFormStateService: SiteFormStateService) => {
      fixture = TestBed.createComponent(BusinessLicenceRenewalPageComponent);
      component = fixture.componentInstance;
      siteFormStateService.setForm(siteService.site);
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
