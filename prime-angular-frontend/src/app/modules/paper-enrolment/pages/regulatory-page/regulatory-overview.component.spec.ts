import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { RegulatoryOverviewComponent } from './regulatory-overview.component';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('RegulatoryOverviewComponent', () => {
  let component: RegulatoryOverviewComponent;
  let fixture: ComponentFixture<RegulatoryOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        RegulatoryOverviewComponent,
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
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
