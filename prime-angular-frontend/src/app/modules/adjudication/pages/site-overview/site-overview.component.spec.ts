import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { PermissionService } from '@auth/shared/services/permission.service';

import { SiteOverviewComponent } from './site-overview.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteOverviewComponent', () => {
  let component: SiteOverviewComponent;
  let fixture: ComponentFixture<SiteOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [SiteOverviewComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        BrowserAnimationsModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: PermissionService,
            useClass: MockPermissionService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
