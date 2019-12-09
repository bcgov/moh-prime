import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { ReviewComponent } from './review.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { ConfigService } from '@config/config.service';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee-review/enrollee-review.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { EnrolleeProfileComponent } from '@shared/components/enrollee-profile/enrollee-profile.component';
import { EnrolleeAddressComponent } from '@shared/components/enrollee-address/enrollee-address.component';
import { EnrolleeSelfDeclarationComponent } from '@shared/components/enrollee-self-declaration/enrollee-self-declaration.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';

describe('ReviewComponent', () => {
  let component: ReviewComponent;
  let fixture: ComponentFixture<ReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule
        ],
        declarations: [
          ReviewComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          EnrolleeReviewComponent,
          EnrolleeProfileComponent,
          EnrolleeAddressComponent,
          EnrolleeSelfDeclarationComponent,
          ProgressIndicatorComponent,
          ConfigCodePipe,
          EnrolmentPipe,
          FormatDatePipe,
          PhonePipe,
          PostalPipe,
          DefaultPipe,
          EnrolleePipe,
          YesNoPipe
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
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
