import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { EnrolleeSelfDeclarationsComponent } from './enrollee-self-declarations.component';

describe('EnrolleeSelfDeclarationsComponent', () => {
  let component: EnrolleeSelfDeclarationsComponent;
  let fixture: ComponentFixture<EnrolleeSelfDeclarationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      declarations: [EnrolleeSelfDeclarationsComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useValue: MockAuthService
        },
        KeycloakService
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeSelfDeclarationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
