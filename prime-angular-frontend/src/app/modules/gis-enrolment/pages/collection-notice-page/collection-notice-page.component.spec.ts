import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthService } from '@auth/shared/services/auth.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { LdapUserPageComponent } from '@gis/pages/ldap-user-page/ldap-user-page.component';
import { CollectionNoticePageComponent } from './collection-notice-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule.withRoutes([
            {
                path: GisEnrolmentRoutes.LDAP_USER_PAGE,
                component: LdapUserPageComponent
            }
        ])],
    providers: [
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
    fixture = TestBed.createComponent(CollectionNoticePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
