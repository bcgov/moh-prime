import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';

import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { PaperEnrolleeReturneeFormComponent } from './paper-enrollee-returnee-form.component';
import { PaperEnrolleeReturneesPageComponent } from './paper-enrollee-returnees-page.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

describe('PaperEnrolleeReturneesComponent', () => {
  let component: PaperEnrolleeReturneesPageComponent;
  let fixture: ComponentFixture<PaperEnrolleeReturneesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        MatDialogModule,
        HttpClientModule,
        MatSnackBarModule,
        ReactiveFormsModule,
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
          provide: EnrolmentService,
          useClass: MockEnrolmentService
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provde: AccessTokenService,
          useClass: MockAccessTokenService
        },
        KeycloakService
      ],
      declarations: [PaperEnrolleeReturneesPageComponent, PaperEnrolleeReturneeFormComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaperEnrolleeReturneesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
