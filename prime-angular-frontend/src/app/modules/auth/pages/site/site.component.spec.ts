import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteComponent } from './site.component';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { MockAuthenticationService } from 'test/mocks/mock-authentication.service';
import { AuthModule } from '@auth/auth.module';

describe('SiteComponent', () => {
  let component: SiteComponent;
  let fixture: ComponentFixture<SiteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
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
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
