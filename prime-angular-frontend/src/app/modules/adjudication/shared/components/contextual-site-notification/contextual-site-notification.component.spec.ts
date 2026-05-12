import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { ContextualSiteNotificationComponent } from './contextual-site-notification.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('ContextualSiteNotificationComponent', () => {
  let component: ContextualSiteNotificationComponent;
  let fixture: ComponentFixture<ContextualSiteNotificationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [ContextualSiteNotificationComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [NgxMaterialModule],
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
    fixture = TestBed.createComponent(ContextualSiteNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
