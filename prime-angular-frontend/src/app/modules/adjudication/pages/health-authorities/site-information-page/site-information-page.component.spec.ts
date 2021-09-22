import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { SiteInformationPageComponent } from './site-information-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

describe('SiteInformationPageComponent', () => {
  let component: SiteInformationPageComponent;
  let fixture: ComponentFixture<SiteInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        BrowserAnimationsModule
      ],
      declarations: [SiteInformationPageComponent],
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
    fixture = TestBed.createComponent(SiteInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
