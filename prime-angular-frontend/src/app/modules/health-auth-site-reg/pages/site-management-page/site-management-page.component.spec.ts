import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthorizedUserService } from 'test/mocks/mock-authorized-user.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { CasePipe } from '@shared/pipes/case.pipe';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { SiteManagementPageComponent } from './site-management-page.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('SiteManagementPageComponent', () => {
  let component: SiteManagementPageComponent;
  let fixture: ComponentFixture<SiteManagementPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      declarations: [
        SiteManagementPageComponent,
        CapitalizePipe,
        CasePipe
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
          provide: AuthorizedUserService,
          useClass: MockAuthorizedUserService
        },
        CapitalizePipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
