import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { HealthAuthorityComponent } from './health-authority.component';

describe('HealthAuthorityComponent', () => {
  let component: HealthAuthorityComponent;
  let fixture: ComponentFixture<HealthAuthorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        SharedModule,
        BrowserAnimationsModule
      ],
      declarations: [],
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
          provide: AccessTokenService,
          useClass: MockAccessTokenService
        },
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
