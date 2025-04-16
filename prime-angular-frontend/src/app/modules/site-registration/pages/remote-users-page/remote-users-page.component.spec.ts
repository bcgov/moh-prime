import { inject, ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockCommunitySiteService } from 'test/mocks/mock-community-site.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { SiteService } from '@registration/shared/services/site.service';
import { RemoteUsersPageComponent } from './remote-users-page.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { LicenseNumberLabelPipe } from '@shared/pipes/license-number-label.pipe';
import { CollegeNamePipe } from '@shared/pipes/college-name.pipe';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { ConfigCodePipe } from '@config/config-code.pipe';

describe('RemoteUsersPageComponent', () => {
  let component: RemoteUsersPageComponent;
  let fixture: ComponentFixture<RemoteUsersPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule,
        MatTooltipModule
      ],
      declarations: [
        RemoteUsersPageComponent,
        AddressPipe,
        FullnamePipe,
      ],
      providers: [
        LicenseNumberLabelPipe,
        CollegeNamePipe,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: SiteService,
          useClass: MockCommunitySiteService
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        AddressPipe,
        ConfigCodePipe,
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUsersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
