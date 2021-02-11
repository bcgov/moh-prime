import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { EnrolleeSelfDeclarationsComponent } from './enrollee-self-declarations.component';

describe('EnrolleeSelfDeclarationsComponent', () => {
  let component: EnrolleeSelfDeclarationsComponent;
  let fixture: ComponentFixture<EnrolleeSelfDeclarationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxMaterialModule,
        SharedModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useValue: MockAuthService
        },
        {
          provide: EnrolmentService,
          useValue: MockEnrolmentService
        },
        KeycloakService
      ]
    })
      .compileComponents();
  });

  beforeEach(inject([EnrolmentService], (enrolmentService: EnrolmentService) => {
    fixture = TestBed.createComponent(EnrolleeSelfDeclarationsComponent);
    component = fixture.componentInstance;
    component.enrolment = enrolmentService.enrolment;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
