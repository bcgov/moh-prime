import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { CollectionNoticePageComponent } from './collection-notice-page.component';

xdescribe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          RouterTestingModule
        ],
        providers: [
          {
            provide: AuthService,
            useClass: MockAuthService
          }
        ],
        schemas: [NO_ERRORS_SCHEMA]
      }
    ).compileComponents();
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
