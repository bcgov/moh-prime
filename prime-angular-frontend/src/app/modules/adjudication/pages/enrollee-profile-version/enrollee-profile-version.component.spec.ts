import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolleeProfileVersionComponent } from './enrollee-profile-version.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee/enrollee-review/enrollee-review.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { YesNoPipe } from '@shared/pipes/yes-no.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';

describe('EnrolleeProfileVersionComponent', () => {
  let component: EnrolleeProfileVersionComponent;
  let fixture: ComponentFixture<EnrolleeProfileVersionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          NgxMaterialModule,
          RouterTestingModule,
          HttpClientTestingModule,
          NgxContextualHelpModule
        ],
        declarations: [
          EnrolleeProfileVersionComponent,
          PageComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          EnrolleeReviewComponent,
          EnrolleeProfileComponent,
          EnrolleePropertyComponent,
          DefaultPipe,
          ConfigCodePipe,
          PostalPipe,
          PhonePipe,
          FormatDatePipe,
          YesNoPipe,
          EnrolleePipe
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
    fixture = TestBed.createComponent(EnrolleeProfileVersionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
