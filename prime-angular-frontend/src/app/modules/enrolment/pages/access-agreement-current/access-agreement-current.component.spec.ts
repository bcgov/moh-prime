import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AccessAgreementCurrentComponent } from './access-agreement-current.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { SharedModule } from '@shared/shared.module';

describe('AccessAgreementCurrentComponent', () => {
  let component: AccessAgreementCurrentComponent;
  let fixture: ComponentFixture<AccessAgreementCurrentComponent>;

  beforeEach(async(() => {
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
      ]
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
