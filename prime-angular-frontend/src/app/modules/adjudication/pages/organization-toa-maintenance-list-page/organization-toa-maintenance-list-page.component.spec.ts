import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { OrganizationToaMaintenanceListPageComponent } from './organization-toa-maintenance-list-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrganizationToaMaintenanceListPageComponent', () => {
  let component: OrganizationToaMaintenanceListPageComponent;
  let fixture: ComponentFixture<OrganizationToaMaintenanceListPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [OrganizationToaMaintenanceListPageComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        NgxMaterialModule,
        BrowserAnimationsModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationToaMaintenanceListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

