import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { MockHealthAuthoritySiteService } from 'test/mocks/mock-health-authority-site.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { RemoteUsersPageComponent } from './remote-users-page.component';

describe('RemoteUsersPageComponent', () => {
  let component: RemoteUsersPageComponent;
  let fixture: ComponentFixture<RemoteUsersPageComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Remote Users',
      },
      queryParams: {
        fromRemoteUser: true
      },
      params: {
        haid: 1,
        sid: 7
      }
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule
      ],
      declarations: [
        RemoteUsersPageComponent,
        FullnamePipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
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
    fixture = TestBed.createComponent(RemoteUsersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
