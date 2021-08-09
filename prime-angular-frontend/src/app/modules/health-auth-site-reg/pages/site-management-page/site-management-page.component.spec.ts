import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { SiteManagementPageComponent } from './site-management-page.component';

describe('SiteManagementPageComponent', () => {
  let component: SiteManagementPageComponent;
  let fixture: ComponentFixture<SiteManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
        imports: [
          // BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          MatSnackBarModule
          // ReactiveFormsModule,
          // NgxMaterialModule
        ],
        declarations: [SiteManagementPageComponent],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: ConfigService,
            useClass: MockConfigService
          },
          // KeycloakService,
          // ConfigCodePipe,
          // FullnamePipe,
          // AddressPipe,
          CapitalizePipe
        ],
        schemas: [NO_ERRORS_SCHEMA]
      }
    ).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
