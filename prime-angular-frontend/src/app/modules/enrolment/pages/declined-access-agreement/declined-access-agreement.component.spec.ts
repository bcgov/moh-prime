import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedAccessAgreementComponent } from './declined-access-agreement.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { PrimeContactComponent } from '@shared/components/prime-contact/prime-contact.component';
import { RouterTestingModule } from '@angular/router/testing';
import { EnrolmentPipe } from '@shared/pipes/enrolment.pipe';

describe('DeclinedAccessAgreementComponent', () => {
  let component: DeclinedAccessAgreementComponent;
  let fixture: ComponentFixture<DeclinedAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule,
          NgxMaterialModule
        ],
        declarations: [
          DeclinedAccessAgreementComponent,
          PageHeaderComponent,
          ProgressIndicatorComponent,
          PrimeContactComponent,
          AlertComponent,
          EnrolmentPipe
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
