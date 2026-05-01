import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigModule } from '@config/config.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { SiteInformationOverviewComponent } from './site-information-overview.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SiteInformationOverviewComponent', () => {
  let component: SiteInformationOverviewComponent;
  let fixture: ComponentFixture<SiteInformationOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        SiteInformationOverviewComponent,
        DefaultPipe
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        MatSnackBarModule,
        ConfigModule],
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
    fixture = TestBed.createComponent(SiteInformationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
