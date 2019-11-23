import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { NgxMaskModule } from 'ngx-mask';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { RegulatoryComponent } from './regulatory.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import {
  CollegeCertificationFormComponent
} from '@enrolment/shared/components/college-certification-form/college-certification-form.component';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

describe('RegulatoryComponent', () => {
  let component: RegulatoryComponent;
  let fixture: ComponentFixture<RegulatoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaskModule,
          NgxMaterialModule,
          ReactiveFormsModule
        ],
        declarations: [
          FormIconGroupComponent,
          RegulatoryComponent,
          PageHeaderComponent,
          PageSubheaderComponent,
          CollegeCertificationFormComponent
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
          EnrolmentStateService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject([EnrolmentStateService], (enrolmentStateService: EnrolmentStateService) => {
    fixture = TestBed.createComponent(RegulatoryComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentStateService.buildRegulatoryForm();
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
