import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthCareTypesPageComponent } from './health-auth-care-types-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('HealthAuthCareTypesPageComponent', () => {
  let component: HealthAuthCareTypesPageComponent;
  let fixture: ComponentFixture<HealthAuthCareTypesPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        HealthAuthCareTypesPageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        ReactiveFormsModule,
        MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        CapitalizePipe,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
