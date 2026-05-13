import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { CollectionNoticePageComponent } from './collection-notice-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [CollectionNoticePageComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule],
    providers: [
        {
            provide: AuthService,
            useClass: MockAuthService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
