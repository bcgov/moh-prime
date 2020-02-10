import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { PharmanetEnrolmentCertificateComponent } from './pharmanet-enrolment-certificate.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';


describe('PharmanetEnrolmentCertificateComponent', () => {
  let component: PharmanetEnrolmentCertificateComponent;
  let fixture: ComponentFixture<PharmanetEnrolmentCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          EnrolmentModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
          {
            provide: ConfigService,
            useClass: MockConfigService
          }
        ]

      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmanetEnrolmentCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
