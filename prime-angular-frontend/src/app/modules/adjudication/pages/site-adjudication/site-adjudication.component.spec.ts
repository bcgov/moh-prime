import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { SiteAdjudicationComponent } from './site-adjudication.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('SiteAdjudicationComponent', () => {
  let component: SiteAdjudicationComponent;
  let fixture: ComponentFixture<SiteAdjudicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
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
    fixture = TestBed.createComponent(SiteAdjudicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
