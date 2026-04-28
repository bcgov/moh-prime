import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { PaperEnrolmentProgressIndicatorComponent } from './paper-enrolment-progress-indicator.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('PaperEnrolmentProgressIndicatorComponent', () => {
  let component: PaperEnrolmentProgressIndicatorComponent;
  let fixture: ComponentFixture<PaperEnrolmentProgressIndicatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [PaperEnrolmentProgressIndicatorComponent],
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
    fixture = TestBed.createComponent(PaperEnrolmentProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
