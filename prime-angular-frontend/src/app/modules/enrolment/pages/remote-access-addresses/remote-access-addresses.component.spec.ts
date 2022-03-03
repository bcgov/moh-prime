import { HttpClientTestingModule } from '@angular/common/http/testing';
import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { RemoteAccessAddressesComponent } from './remote-access-addresses.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('RemoteAccessAddressesComponent', () => {
  let component: RemoteAccessAddressesComponent;
  let fixture: ComponentFixture<RemoteAccessAddressesComponent>;

  const mockActivatedRoute = {
    snapshot: {
      routeConfig: EnrolmentRoutes.SELF_DECLARATION
    }
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([]),
        NgxMaterialModule,
        ReactiveFormsModule,
        BrowserAnimationsModule
      ],
      declarations: [RemoteAccessAddressesComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteAccessAddressesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
