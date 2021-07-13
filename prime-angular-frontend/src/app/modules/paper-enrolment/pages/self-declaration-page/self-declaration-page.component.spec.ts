import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { SelfDeclarationPageComponent } from './self-declaration-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { ActivatedRoute } from '@angular/router';

describe('SelfDeclarationPageComponent', () => {
  let component: SelfDeclarationPageComponent;
  let fixture: ComponentFixture<SelfDeclarationPageComponent>;
  const mockActivatedRoute = {
    snapshot: { params: { eid: 1 } }
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [SelfDeclarationPageComponent],
        imports: [
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: AuthService,
            useClass: MockAuthService
          },
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
          {
            provide: ConfigService,
            useClass: MockConfigService
          },
          {
            provide: ActivatedRoute,
            useValue: mockActivatedRoute
          },
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelfDeclarationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
