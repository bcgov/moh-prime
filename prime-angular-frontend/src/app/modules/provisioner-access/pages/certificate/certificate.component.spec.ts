import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';

import { HttpClientTestingModule } from '@angular/common/http/testing';

import { ClipboardModule } from 'ngx-clipboard';
import { CertificateComponent } from './certificate.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { ProvisionerAccessModule } from '../../provisioner-access.module';
import { CoreModule } from '@core/core.module';

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
          HttpClientTestingModule,
          ProvisionerAccessModule,
          CoreModule
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
