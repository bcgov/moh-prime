import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { AccessAgreementCurrentComponent } from './access-agreement-current.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { SharedModule } from '@shared/shared.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('AccessAgreementCurrentComponent', () => {
  let component: AccessAgreementCurrentComponent;
  let fixture: ComponentFixture<AccessAgreementCurrentComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxBusyModule,
        NgxMaterialModule,
        NgxContextualHelpModule,
        HttpClientTestingModule,
        RouterTestingModule,
        EnrolmentModule,
        SharedModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: EnrolmentService,
          useClass: MockEnrolmentService
        },
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementCurrentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
