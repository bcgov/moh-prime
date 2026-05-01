import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AdminUsersContainerComponent } from './admin-users-container.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AdminUsersContainerComponent', () => {
  let component: AdminUsersContainerComponent;
  let fixture: ComponentFixture<AdminUsersContainerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
    declarations: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        AdjudicationModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminUsersContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
