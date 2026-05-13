import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { HealthAuthCareTypeOverviewComponent } from './health-auth-care-type-overview.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('HealthAuthCareTypeOverviewComponent', () => {
  let component: HealthAuthCareTypeOverviewComponent;
  let fixture: ComponentFixture<HealthAuthCareTypeOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        HealthAuthCareTypeOverviewComponent,
        DefaultPipe,
        ConfigCodePipe,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        MatSnackBarModule],
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
    fixture = TestBed.createComponent(HealthAuthCareTypeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
