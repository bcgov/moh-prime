import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';


import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { AdjudicatorNotesComponent } from './adjudicator-notes.component';


describe('AdjudicatorNotesComponent', () => {
  let component: AdjudicatorNotesComponent;
  let fixture: ComponentFixture<AdjudicatorNotesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule(
      {
        imports: [
          ReactiveFormsModule,
          RouterTestingModule,
          HttpClientTestingModule,
          NgxMaterialModule,
          BrowserAnimationsModule
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
          },
          {
            provide: PermissionService,
            useClass: MockPermissionService
          }
        ],
        schemas: [NO_ERRORS_SCHEMA]
      }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
