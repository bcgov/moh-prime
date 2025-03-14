import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { AccessTermsComponent } from './access-terms.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('AccessTermsComponent', () => {
  let component: AccessTermsComponent;
  let fixture: ComponentFixture<AccessTermsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          NgxMaterialModule,
          RouterTestingModule,
          HttpClientTestingModule,
          RouterTestingModule,
          EnrolmentModule
        ],
        declarations: [
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          }
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
