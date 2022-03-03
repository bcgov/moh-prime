import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { AccessDeclinedComponent } from './access-declined.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('AccessDeclinedComponent', () => {
  let component: AccessDeclinedComponent;
  let fixture: ComponentFixture<AccessDeclinedComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        EnrolmentModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessDeclinedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
