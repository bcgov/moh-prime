import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { OrganizationClaimPageComponent } from './organization-claim-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrganizationClaimPageComponent', () => {
  let component: OrganizationClaimPageComponent;
  let fixture: ComponentFixture<OrganizationClaimPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        OrganizationClaimPageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatSnackBarModule],
    providers: [
        KeycloakService,
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationClaimPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
