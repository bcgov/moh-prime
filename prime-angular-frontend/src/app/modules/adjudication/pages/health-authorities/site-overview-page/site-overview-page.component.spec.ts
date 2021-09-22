import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { PermissionService } from '@auth/shared/services/permission.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { SiteOverviewPageComponent } from './site-overview-page.component';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

describe('SiteOverviewPageComponent', () => {
  let component: SiteOverviewPageComponent;
  let fixture: ComponentFixture<SiteOverviewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        BrowserAnimationsModule
      ],
      declarations: [SiteOverviewPageComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: PermissionService,
          useClass: MockPermissionService
        },
        CapitalizePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteOverviewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
