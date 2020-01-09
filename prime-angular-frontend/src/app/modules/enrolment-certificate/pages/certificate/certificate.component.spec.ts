import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';

import { CertificateComponent } from './certificate.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { ClipboardIconComponent } from '@shared/components/clipboard-icon/clipboard-icon.component';
import { CertificatePipe } from '@shared/pipes/certificate.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { ClipboardModule } from 'ngx-clipboard';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EnrolleeProfileComponent } from '@shared/components/enrollee-profile/enrollee-profile.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { EnrolleePrivilegesComponent } from '@shared/components/enrollee-privileges/enrollee-privileges.component';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { EnrolleeOrganizationsComponent } from '@shared/components/enrollee-organizations/enrollee-organizations.component';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { OrganizationComponent } from '@enrolment/pages/organization/organization.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';

describe('CertificateComponent', () => {
  let component: CertificateComponent;
  let fixture: ComponentFixture<CertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          NgxContextualHelpModule,
          ClipboardModule,
          NgxMaterialModule,
          RouterTestingModule,
          HttpClientTestingModule
        ],
        declarations: [
          CertificateComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          EnrolleeProfileComponent,
          ClipboardIconComponent,
          EnrolleePrivilegesComponent,
          EnrolleeOrganizationsComponent,
          ProgressIndicatorComponent,
          CertificatePipe,
          FormatDatePipe,
          DefaultPipe,
          EnrolleePipe,
          ConfigCodePipe
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
    fixture = TestBed.createComponent(CertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
