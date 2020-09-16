import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { AuthComponent } from './auth.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

describe('AuthComponent', () => {
  let component: AuthComponent;
  let fixture: ComponentFixture<AuthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          AuthComponent
        ],
        imports: [
          RouterTestingModule,
          NgxMaterialModule,
          NgxProgressModule,
          DashboardModule
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
