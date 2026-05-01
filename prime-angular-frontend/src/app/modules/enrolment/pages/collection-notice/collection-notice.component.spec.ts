import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { CollectionNoticeComponent } from './collection-notice.component';
import { SharedModule } from '@shared/shared.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { OverviewComponent } from '@enrolment/pages/overview/overview.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppRoutes } from 'app/app.routes';
import { UnderagedComponent } from '@lib/modules/root-routes/components/underaged/underaged.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('CollectionNoticeComponent', () => {
  let component: CollectionNoticeComponent;
  let fixture: ComponentFixture<CollectionNoticeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule.withRoutes([
            {
                path: EnrolmentRoutes.OVERVIEW,
                component: OverviewComponent
            },
            {
                path: AppRoutes.UNDERAGED,
                component: UnderagedComponent
            }
        ]),
        NgxMaterialModule,
        NgxBusyModule,
        EnrolmentModule,
        SharedModule],
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
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
