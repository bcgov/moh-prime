import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteProgressIndicatorComponent } from './site-progress-indicator.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteProgressIndicatorComponent', () => {
  let component: SiteProgressIndicatorComponent;
  let fixture: ComponentFixture<SiteProgressIndicatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        SiteProgressIndicatorComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [SharedModule,
        RouterTestingModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: OrganizationService,
            useClass: MockOrganizationService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
