import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { BehaviorSubject } from 'rxjs';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SharedModule } from '@shared/shared.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { JobFormComponent } from './job-form.component';

describe('JobFormComponent', () => {
  let component: JobFormComponent;
  let fixture: ComponentFixture<JobFormComponent>;

  beforeEach(async(() => {
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
          EnrolmentFormStateService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject(
    [EnrolmentFormStateService, ConfigService],
    (enrolmentFormStateService: EnrolmentFormStateService, configService: ConfigService) => {
      fixture = TestBed.createComponent(JobFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      component.form = enrolmentFormStateService.buildJobForm();
      component.jobNames = new BehaviorSubject(configService.jobNames);
      fixture.detectChanges();
    }
  ));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
