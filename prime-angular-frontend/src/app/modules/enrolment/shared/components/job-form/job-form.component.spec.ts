import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { JobFormComponent } from './job-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('JobFormComponent', () => {
  let component: JobFormComponent;
  let fixture: ComponentFixture<JobFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          ReactiveFormsModule
        ],
        declarations: [
          FormIconGroupComponent,
          JobFormComponent
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
          EnrolmentStateService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject(
    [EnrolmentStateService, ConfigService],
    (enrolmentStateService: EnrolmentStateService, configService: ConfigService) => {
      fixture = TestBed.createComponent(JobFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      component.form = enrolmentStateService.buildJobForm();
      component.jobNames = configService.jobNames;
      fixture.detectChanges();
    }
  ));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
