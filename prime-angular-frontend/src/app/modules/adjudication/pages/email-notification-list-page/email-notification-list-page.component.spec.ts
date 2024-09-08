import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EmailNotificationListPageComponent } from './email-notification-list-page.component';

describe('EmailNotificationListPageComponent', () => {
  let component: EmailNotificationListPageComponent;
  let fixture: ComponentFixture<EmailNotificationListPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule
      ],
      declarations: [
        EmailNotificationListPageComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onBack()', () => {
    it('should navigate with \'../\'', () => {
      const spyOnRouteRelativeTo = spyOn((component as any).routeUtils, 'routeRelativeTo');

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledTimes(1);
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(['../']);
    });
  });
});
