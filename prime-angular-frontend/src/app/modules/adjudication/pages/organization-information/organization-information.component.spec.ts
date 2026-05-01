import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { OrganizationInformationComponent } from './organization-information.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrganizationInformationComponent', () => {
  let component: OrganizationInformationComponent;
  let fixture: ComponentFixture<OrganizationInformationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [OrganizationInformationComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
