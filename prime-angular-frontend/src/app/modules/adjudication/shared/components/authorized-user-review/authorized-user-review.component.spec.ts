import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { AuthorizedUserReviewComponent } from './authorized-user-review.component';

describe('AuthorizedUserReviewComponent', () => {
  let component: AuthorizedUserReviewComponent;
  let fixture: ComponentFixture<AuthorizedUserReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        NgxBusyModule,
        NgxContextualHelpModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        RouterTestingModule,
        AdjudicationModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useValue: MockConfigService
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
