import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { DeclinedComponent } from './declined.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { AlertComponent } from '@shared/components/alert/alert.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { ProgressIndicatorComponent } from '@shared/components/progress-indicator/progress-indicator.component';
import { PrimeContactComponent } from '@shared/components/prime-contact/prime-contact.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

describe('DeclinedComponent', () => {
  let component: DeclinedComponent;
  let fixture: ComponentFixture<DeclinedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule,
          NgxMaterialModule
        ],
        declarations: [
          DeclinedComponent,
          PageComponent,
          PageHeaderComponent,
          PrimeContactComponent,
          ProgressIndicatorComponent,
          AlertComponent
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
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
