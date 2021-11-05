import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';

import { KeycloakService } from 'keycloak-angular';

import { MockHealthAuthorityService } from 'test/mocks/mock-health-authority.service';
import { MockHealthAuthoritySiteService } from 'test/mocks/mock-health-authority-site.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthorityService } from '@health-auth/shared/services/health-authority.service';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { TechnicalSupportPageComponent } from './technical-support-page.component';

describe('TechnicalSupportPageComponent', () => {
  let component: TechnicalSupportPageComponent;
  let fixture: ComponentFixture<TechnicalSupportPageComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Technical Support',
      },
      params: {
        haid: 1,
        sid: 7
      }
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        TechnicalSupportPageComponent
      ],
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatSnackBarModule
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        },
        {
          provide: HealthAuthorityService,
          useClass: MockHealthAuthorityService
        },
        {
          provide: HealthAuthoritySiteService,
          useClass: MockHealthAuthoritySiteService
        },
        CapitalizePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
