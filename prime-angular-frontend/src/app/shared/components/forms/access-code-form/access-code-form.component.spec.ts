import { HttpClientModule } from '@angular/common/http';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, inject, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ConfigService } from '@config/config.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { BehaviorSubject } from 'rxjs';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { AccessCodeFormComponent } from './access-code-form.component';

describe('AccessCodeFormComponent', () => {
  let component: AccessCodeFormComponent;
  let fixture: ComponentFixture<AccessCodeFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        HttpClientTestingModule
      ],
      declarations: [AccessCodeFormComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        KeycloakService
      ]
    }).compileComponents();
  }));

  beforeEach(inject([EnrolmentFormStateService], (enrolmentFormStateService: EnrolmentFormStateService) => {
    fixture = TestBed.createComponent(AccessCodeFormComponent);
    component = fixture.componentInstance;
    component.form = enrolmentFormStateService.accessForm;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
