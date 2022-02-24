import { SharedModule } from '@shared/shared.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { BcscProfileComponent } from './bcsc-profile.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('BcscProfileComponent', () => {
  let component: BcscProfileComponent;
  let fixture: ComponentFixture<BcscProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        HttpClientTestingModule,
        SharedModule
      ],
      declarations: [
        BcscProfileComponent,
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BcscProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
