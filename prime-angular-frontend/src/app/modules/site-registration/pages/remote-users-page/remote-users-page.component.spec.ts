import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockSiteService } from 'test/mocks/mock-site.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AddressPipe } from '@shared/pipes/address.pipe';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { SiteService } from '@registration/shared/services/site.service';
import { RemoteUsersPageComponent } from './remote-users-page.component';

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
        BrowserAnimationsModule
      ],
      declarations: [
        RemoteUsersPageComponent,
        AddressPipe,
        FullnamePipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: SiteService,
          useClass: MockSiteService
        },
        AddressPipe,
        KeycloakService
      ],
      schemas: [NO_ERRORS_SCHEMA]
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
