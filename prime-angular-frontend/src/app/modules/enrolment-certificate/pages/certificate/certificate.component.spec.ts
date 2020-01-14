import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';

import { HttpClientTestingModule } from '@angular/common/http/testing';

import { ClipboardModule } from 'ngx-clipboard';
import { CertificateComponent } from './certificate.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { ClipboardIconComponent } from '@shared/components/clipboard-icon/clipboard-icon.component';
import { CertificatePipe } from '@shared/pipes/certificate.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EnrolleeProfileComponent } from '@shared/components/enrollee/enrollee-profile/enrollee-profile.component';
import { EnrolleePrivilegesComponent } from '@shared/components/enrollee/enrollee-privileges/enrollee-privileges.component';
import { EnrolleeOrganizationsComponent } from '@shared/components/enrollee/enrollee-organizations/enrollee-organizations.component';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { PostalPipe } from '@shared/pipes/postal.pipe';

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
          EnrolleePropertyComponent,
          CertificatePipe,
          FormatDatePipe,
          DefaultPipe,
          EnrolleePipe,
          ConfigCodePipe,
          PostalPipe
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
          },
          ConfigCodePipe
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
