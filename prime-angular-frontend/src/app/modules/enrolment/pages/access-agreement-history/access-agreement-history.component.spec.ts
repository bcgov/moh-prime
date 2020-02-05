import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessAgreementHistoryComponent } from './access-agreement-history.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { RouterModule } from '@angular/router';

describe('AccessAgreementHistoryComponent', () => {
  let component: AccessAgreementHistoryComponent;
  let fixture: ComponentFixture<AccessAgreementHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          EnrolmentModule,
          HttpClientTestingModule,
          RouterModule.forRoot([])
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
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
