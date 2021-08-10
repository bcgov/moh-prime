import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { InRolePipe } from '@shared/pipes/in-role-pipe';
import { NotificationEmailViewComponent } from './notification-email-view.component';

describe('NotificationEmailViewComponent', () => {
  let component: NotificationEmailViewComponent;
  let fixture: ComponentFixture<NotificationEmailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatSnackBarModule
      ],
      declarations: [
        NotificationEmailViewComponent,
        InRolePipe
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        InRolePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationEmailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
