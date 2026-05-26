import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { SatEformsProgressIndicatorComponent } from './sat-eforms-progress-indicator.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('SatEformsProgressIndicatorComponent', () => {
  let component: SatEformsProgressIndicatorComponent;
  let fixture: ComponentFixture<SatEformsProgressIndicatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [SatEformsProgressIndicatorComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [SharedModule,
        RouterTestingModule],
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
    fixture = TestBed.createComponent(SatEformsProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
