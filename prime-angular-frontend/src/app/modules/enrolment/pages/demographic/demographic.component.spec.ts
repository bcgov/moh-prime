import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaskModule } from 'ngx-mask';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { DemographicComponent } from './demographic.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { AddressComponent } from '@shared/components/forms/address/address.component';
import { PageFooterComponent } from '@enrolment/shared/components/page-footer/page-footer.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';

describe('DemographicComponent', () => {
  let component: DemographicComponent;
  let fixture: ComponentFixture<DemographicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaskModule.forRoot(),
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule
        ],
        declarations: [
          DemographicComponent,
          AddressComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          ProgressIndicatorComponent,
          EnrolleeProfileComponent,
          PageFooterComponent,
          DefaultPipe,
          EnrolleePipe
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
            provide: AuthService,
            useClass: MockAuthService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DemographicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
