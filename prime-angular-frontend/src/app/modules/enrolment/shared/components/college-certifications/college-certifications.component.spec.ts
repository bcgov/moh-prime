import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { CollegeCertificationsComponent } from './college-certifications.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

describe('CollegeCertificationsComponent', () => {
  let component: CollegeCertificationsComponent;
  let fixture: ComponentFixture<CollegeCertificationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule
      ],
      declarations: [
        CollegeCertificationsComponent
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

  beforeEach(async(inject([ConfigService], (configService: ConfigService) => {
    // Load the runtime configuration
    configService.load().subscribe();
  })));

  beforeEach(inject([EnrolmentStateService], (enrolmentStateService: EnrolmentStateService) => {
    fixture = TestBed.createComponent(CollegeCertificationsComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentStateService.buildCollegeCertificationForm();
    fixture.detectChanges();
  }));

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
