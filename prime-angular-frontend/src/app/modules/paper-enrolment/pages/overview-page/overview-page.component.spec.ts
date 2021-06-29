import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { OverviewPageComponent } from './overview-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';

describe('OverviewComponent', () => {
  let component: OverviewPageComponent;
  let fixture: ComponentFixture<OverviewPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [OverviewPageComponent],
        imports: [
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule,
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
          {
            provide: AuthService,
            useClass: MockAuthService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverviewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
