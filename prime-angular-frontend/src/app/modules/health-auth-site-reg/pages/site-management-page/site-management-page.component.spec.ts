import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthorizedUserService } from 'test/mocks/mock-authorized-user.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';
import { SiteManagementPageComponent } from './site-management-page.component';

describe('SiteManagementPageComponent', () => {
  let component: SiteManagementPageComponent;
  let fixture: ComponentFixture<SiteManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
        imports: [
          HttpClientTestingModule,
          RouterTestingModule,
          NgxMaterialModule
        ],
        declarations: [
          SiteManagementPageComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: ConfigService,
            useClass: MockConfigService
          },
          {
            provide: AuthorizedUserService,
            useClass: MockAuthorizedUserService
          },
          CapitalizePipe
        ],
        schemas: [NO_ERRORS_SCHEMA]
      }
    ).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
