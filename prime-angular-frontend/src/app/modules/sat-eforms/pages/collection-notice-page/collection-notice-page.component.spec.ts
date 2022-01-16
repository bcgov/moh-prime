import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { CollectionNoticePageComponent } from './collection-notice-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
      ],
      declarations: [CollectionNoticePageComponent],
      providers: [
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
