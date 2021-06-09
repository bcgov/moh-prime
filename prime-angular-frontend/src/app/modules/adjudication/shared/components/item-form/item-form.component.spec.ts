import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { BehaviorSubject } from 'rxjs';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { ItemFormComponent } from './item-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SharedModule } from '@shared/shared.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

describe('JobFormComponent', () => {
  let component: ItemFormComponent;
  let fixture: ComponentFixture<ItemFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          SharedModule,
          EnrolmentModule
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
            provide: AuthService,
            useClass: MockAuthService
          },
          EnrolmentFormStateService,
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject(
    [EnrolmentFormStateService, ConfigService],
    (enrolmentFormStateService: EnrolmentFormStateService, configService: ConfigService) => {
      fixture = TestBed.createComponent(ItemFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      // component.form = enrolmentFormStateService.buildJobForm();
      component.jobNames = new BehaviorSubject(configService.jobNames);
      fixture.detectChanges();
    }
  ));

  // Deprecated Component
  //
  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
