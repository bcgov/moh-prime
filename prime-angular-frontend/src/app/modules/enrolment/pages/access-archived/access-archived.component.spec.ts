import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

import { AccessArchivedComponent } from './access-archived.component';

describe('AccessArchivedComponent', () => {
  let component: AccessArchivedComponent;
  let fixture: ComponentFixture<AccessArchivedComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        schemas: [CUSTOM_ELEMENTS_SCHEMA],
        imports: [RouterTestingModule,
          NgxMaterialModule,
          NgxBusyModule,
          EnrolmentModule],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
          provideHttpClient(withInterceptorsFromDi()),
          provideHttpClientTesting(),
        ]
      }
    ).compileComponents();
  }));


  beforeEach(() => {
    fixture = TestBed.createComponent(AccessArchivedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
