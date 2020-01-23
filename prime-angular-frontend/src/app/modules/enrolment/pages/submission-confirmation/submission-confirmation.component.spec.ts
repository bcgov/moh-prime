import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SubmissionConfirmationComponent } from './submission-confirmation.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { PrimeContactComponent } from '@shared/components/prime-contact/prime-contact.component';
import { CollectionNoticeAlertComponent } from '@enrolment/shared/components/collection-notice-alert/collection-notice-alert.component';
import { PrimePhoneComponent } from '@shared/components/prime-phone/prime-phone.component';
import { PrimeEmailComponent } from '@shared/components/prime-email/prime-email.component';
import { KeycloakService } from 'keycloak-angular';

describe('SubmissionConfirmationComponent', () => {
  let component: SubmissionConfirmationComponent;
  let fixture: ComponentFixture<SubmissionConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          RouterTestingModule,
        ],
        declarations: [
          AlertComponent,
          SubmissionConfirmationComponent,
          PageComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          PrimePhoneComponent,
          PrimeEmailComponent,
          ProgressIndicatorComponent,
          PrimeContactComponent,
          CollectionNoticeAlertComponent
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
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
