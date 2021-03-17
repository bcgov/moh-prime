import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { CollectionNoticePageComponent } from './collection-notice-page.component';

// import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
// import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
// import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
// import { LdapUserPageComponent } from '../ldap-user-page/ldap-user-page.component';

describe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          // RouterTestingModule.withRoutes([
          //   {
          //     path: GisEnrolmentRoutes.LDAP_USER_PAGE,
          //     component: LdapUserPageComponent
          //   }
          // ]),
          // NgxMaterialModule,
          // NgxBusyModule
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
            provide: AuthService,
            useClass: MockAuthService
          }
        ],
        schemas: [NO_ERRORS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
