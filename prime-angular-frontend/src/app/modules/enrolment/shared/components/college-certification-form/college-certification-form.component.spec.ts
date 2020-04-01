import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaskModule } from 'ngx-mask';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { CollegeCertificationFormComponent } from './college-certification-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CollegeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

describe('CollegeCertificationFormComponent', () => {
  let component: CollegeCertificationFormComponent;
  let fixture: ComponentFixture<CollegeCertificationFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        NgxContextualHelpModule,
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaskModule.forRoot(),
        NgxMaterialModule,
        ReactiveFormsModule
      ],
      declarations: [
        CollegeCertificationFormComponent,
        FormIconGroupComponent
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
    }).compileComponents();
  }));

  beforeEach(inject(
    [EnrolmentStateService, ConfigService],
    (enrolmentStateService: EnrolmentStateService, configService: ConfigService
    ) => {
      fixture = TestBed.createComponent(CollegeCertificationFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      component.form = enrolmentStateService.buildCollegeCertificationForm();
      component.selectedColleges = configService.colleges.map((college: CollegeConfig) => college.code);
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
