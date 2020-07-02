import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { SiteRegistrationComponent } from './site-registration.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('SiteRegistrationComponent', () => {
  let component: SiteRegistrationComponent;
  let fixture: ComponentFixture<SiteRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        NgxBusyModule,
        NgxContextualHelpModule,
        NgxMaterialModule,
        RouterTestingModule,
        AdjudicationModule
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
        AuthenticationService,
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
