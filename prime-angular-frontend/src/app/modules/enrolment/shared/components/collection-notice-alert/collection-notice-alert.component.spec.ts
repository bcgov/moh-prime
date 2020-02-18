import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { CollectionNoticeAlertComponent } from './collection-notice-alert.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { RouterTestingModule } from '@angular/router/testing';
import { PrimeEmailComponent } from '@shared/components/prime-email/prime-email.component';
import { PrimePhoneComponent } from '@shared/components/prime-phone/prime-phone.component';
import { SharedModule } from '@shared/shared.module';

describe('CollectionNoticeAlertComponent', () => {
  let component: CollectionNoticeAlertComponent;
  let fixture: ComponentFixture<CollectionNoticeAlertComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          RouterTestingModule,
          SharedModule
        ],
        declarations: [
          AlertComponent,
          CollectionNoticeAlertComponent,
          PrimeEmailComponent,
          PrimePhoneComponent,
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
    fixture = TestBed.createComponent(CollectionNoticeAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
