import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { PermissionService } from '@auth/shared/services/permission.service';
import { InRolePipe } from '@shared/pipes/in-role-pipe';
import { SiteRegistrationTableComponent } from './site-registration-table.component';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { MatTableDataSource } from '@angular/material/table';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';

describe('SiteRegistrationTableComponent', () => {
  let component: SiteRegistrationTableComponent;
  let fixture: ComponentFixture<SiteRegistrationTableComponent>;
  const mockActivatedRoute = {
    snapshot: {
      params: {
        sid: 1
      }
    }
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxMaterialModule,
        RouterTestingModule,
        ReactiveFormsModule,
        BrowserAnimationsModule
      ],
      declarations: [
        InRolePipe
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
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationTableComponent);
    component = fixture.componentInstance;
    component.dataSource = new MatTableDataSource<SiteRegistrationListViewModel>();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
