import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolleeReviewComponent } from './enrollee-review.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { EnrolleeAddressComponent } from '@shared/components/enrollee/enrollee-address/enrollee-address.component';
import { EnrolleeSelfDeclarationComponent } from '@shared/components/enrollee/enrollee-self-declaration/enrollee-self-declaration.component';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';
import { EnrolleeOrganizationsComponent } from '../enrollee-organizations/enrollee-organizations.component';

describe('EnrolleeReviewComponent', () => {
  let component: EnrolleeReviewComponent;
  let fixture: ComponentFixture<EnrolleeReviewComponent>;

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
          PageSubheaderComponent,
          EnrolleeReviewComponent,
          EnrolleeProfileComponent,
          EnrolleeAddressComponent,
          EnrolleeSelfDeclarationComponent,
          EnrolleeOrganizationsComponent,
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
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
