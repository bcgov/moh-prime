import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { DashboardV1Component } from './dashboardv1.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { HeaderComponent } from '../header/header.component';

describe('DashboardComponent', () => {
  let component: DashboardV1Component;
  let fixture: ComponentFixture<DashboardV1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          RouterTestingModule,
          NgxMaterialModule,
          NgxProgressModule
        ],
        declarations: [
          DashboardV1Component,
          HeaderComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: AuthService,
            useClass: MockAuthService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
