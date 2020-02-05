import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AdminComponent } from './admin.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { AuthModule } from '@auth/auth.module';

describe('AdminComponent', () => {
  let component: AdminComponent;
  let fixture: ComponentFixture<AdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule,
          AuthModule
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
    fixture = TestBed.createComponent(AdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
