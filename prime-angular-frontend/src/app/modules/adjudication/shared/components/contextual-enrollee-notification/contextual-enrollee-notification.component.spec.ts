import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { ContextualEnrolleeNotificationComponent } from './contextual-enrollee-notification.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('ContextualEnrolleeNotificationComponent', () => {
  let component: ContextualEnrolleeNotificationComponent;
  let fixture: ComponentFixture<ContextualEnrolleeNotificationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [ContextualEnrolleeNotificationComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting(),
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContextualEnrolleeNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
