import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SiteRemoteUsersComponent } from './site-remote-users.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteRemoteUsersComponent', () => {
  let component: SiteRemoteUsersComponent;
  let fixture: ComponentFixture<SiteRemoteUsersComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Remote Users'
      },
      params: {
        sid: 1
      }
    }
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [SiteRemoteUsersComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: ActivatedRoute,
            useValue: mockActivatedRoute
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRemoteUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
