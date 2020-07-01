import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

import { CollectionNoticeContainerComponent } from './collection-notice-container.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from '@shared/shared.module';

describe('CollectionNoticeContainerComponent', () => {
  let component: CollectionNoticeContainerComponent;
  let fixture: ComponentFixture<CollectionNoticeContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          RouterTestingModule,
          SharedModule
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
            provide: AuthenticationService,
            useClass: MockAuthenticationService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticeContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
