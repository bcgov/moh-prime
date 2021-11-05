import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockCommunitySiteService } from 'test/mocks/mock-community-site.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { BusinessLicenceRenewalPageComponent } from './business-licence-renewal-page.component';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';

describe('BusinessLicenceRenewalPageComponent', () => {
  let component: BusinessLicenceRenewalPageComponent;
  let fixture: ComponentFixture<BusinessLicenceRenewalPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule
      ],
      declarations: [
        BusinessLicenceRenewalPageComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: SiteService,
          useClass: MockCommunitySiteService
        },
        SiteFormStateService
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

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
