import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { ProfessionalInfoComponent } from './professional-info.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { SubHeaderComponent } from '@shared/components/sub-header/sub-header.component';
import { CollegeCertificationsComponent } from '@enrolment/shared/components/college-certifications/college-certifications.component';

describe('ProfessionalInfoComponent', () => {
  let component: ProfessionalInfoComponent;
  let fixture: ComponentFixture<ProfessionalInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule
        ],
        declarations: [
          ProfessionalInfoComponent,
          SubHeaderComponent,
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
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfessionalInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
