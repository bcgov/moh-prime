import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteInformationFormComponent } from './site-information-form.component';

describe('SiteInformationFormComponent', () => {
  let component: SiteInformationFormComponent;
  let fixture: ComponentFixture<SiteInformationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SiteInformationFormComponent],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule,
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        SiteFormStateService,
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(inject([SiteFormStateService], (siteFormStateService: SiteFormStateService) => {
    fixture = TestBed.createComponent(SiteInformationFormComponent);
    component = fixture.componentInstance;
    component.form = siteFormStateService.businessLicenceFormState.form;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
