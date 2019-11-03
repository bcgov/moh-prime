import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { InfoComponent } from './info.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';

describe('InfoComponent', () => {
  let component: InfoComponent;
  let fixture: ComponentFixture<InfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          InfoComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: AuthService,
            useValue: MockAuthService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });
});
