import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { UnlicensedStudentNurseFormComponent } from './unlicensed-student-nurse-form.component';
import { ReactiveFormsModule, UntypedFormBuilder } from '@angular/forms';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { RegulatoryFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { MatDatepickerModule } from '@angular/material/datepicker';

describe('UnlicensedStudentNurseFormComponent', () => {
  let component: UnlicensedStudentNurseFormComponent;
  let fixture: ComponentFixture<UnlicensedStudentNurseFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnlicensedStudentNurseFormComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
      imports: [BrowserAnimationsModule,
        NgxContextualHelpModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        MatDatepickerModule],
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
          provide: AuthService,
          useClass: MockAuthService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
      ]
    })
      .compileComponents();
  });

  beforeEach(inject(
    [UntypedFormBuilder],
    (
      fb: UntypedFormBuilder
    ) => {
      fixture = TestBed.createComponent(UnlicensedStudentNurseFormComponent);
      component = fixture.componentInstance;
      const formState = new RegulatoryFormState(fb, TestBed.inject(ConfigService));
      component.form = formState.buildUnlicensedStudentNurseForm();
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
