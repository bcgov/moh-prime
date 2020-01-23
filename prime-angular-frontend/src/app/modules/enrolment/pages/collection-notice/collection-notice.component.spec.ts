import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { CollectionNoticeComponent } from './collection-notice.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { PrimeContactComponent } from '@shared/components/prime-contact/prime-contact.component';
import { CollectionNoticeAlertComponent } from '@enrolment/shared/components/collection-notice-alert/collection-notice-alert.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { PrimePhoneComponent } from '@shared/components/prime-phone/prime-phone.component';
import { PrimeEmailComponent } from '@shared/components/prime-email/prime-email.component';

describe('CollectionNoticeComponent', () => {
  let component: CollectionNoticeComponent;
  let fixture: ComponentFixture<CollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule,
          NgxMaterialModule,
          NgxBusyModule
        ],
        declarations: [
          AlertComponent,
          CollectionNoticeComponent,
          CollectionNoticeAlertComponent,
          PrimeContactComponent,
          PrimeEmailComponent,
          PrimePhoneComponent,
          PageComponent,
          PageHeaderComponent
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
            provide: AuthService,
            useClass: MockAuthService
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
