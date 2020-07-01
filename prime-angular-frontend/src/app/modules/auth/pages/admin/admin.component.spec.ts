import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';

import { AdminComponent } from './admin.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { AuthModule } from '@auth/auth.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

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
            provide: AuthenticationService,
            useClass: MockAuthenticationService
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
