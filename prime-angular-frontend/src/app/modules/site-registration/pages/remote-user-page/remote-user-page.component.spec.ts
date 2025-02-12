import { ComponentFixture, inject, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { MockCommunitySiteService } from 'test/mocks/mock-community-site.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { RemoteUserPageComponent } from './remote-user-page.component';

describe('RemoteUserPageComponent', () => {
  let component: RemoteUserPageComponent;
  let fixture: ComponentFixture<RemoteUserPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([
          {
            path: 'new',
            component: RemoteUserPageComponent
          }
        ]),
        ReactiveFormsModule,
        NgxMaterialModule
      ],
      declarations: [RemoteUserPageComponent],
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
          provide: SiteService,
          useClass: MockCommunitySiteService
        },
        SiteFormStateService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject(
    [SiteService, SiteFormStateService],
    (siteService: SiteService, siteFormStateService: SiteFormStateService) => {
      fixture = TestBed.createComponent(RemoteUserPageComponent);
      component = fixture.componentInstance;
      siteFormStateService.setForm(siteService.site);
      fixture.detectChanges();
    })
  );

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
