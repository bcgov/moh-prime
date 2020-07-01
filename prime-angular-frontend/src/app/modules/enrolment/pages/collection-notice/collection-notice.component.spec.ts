import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { CollectionNoticeComponent } from './collection-notice.component';
import { SharedModule } from '@shared/shared.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('CollectionNoticeComponent', () => {
  let component: CollectionNoticeComponent;
  let fixture: ComponentFixture<CollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          RouterTestingModule,
          NgxMaterialModule,
          NgxBusyModule,
          EnrolmentModule,
          SharedModule
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
          {
            provide: AuthenticationService,
            useClass: MockAuthenticationService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
