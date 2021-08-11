import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { GisEnrolmentProgressIndicatorComponent } from './gis-enrolment-progress-indicator.component';

xdescribe('GisEnrolmentProgressIndicatorComponent', () => {
  let component: GisEnrolmentProgressIndicatorComponent;
  let fixture: ComponentFixture<GisEnrolmentProgressIndicatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        SharedModule,
        HttpClientTestingModule,
        RouterTestingModule
      ],
      declarations: [GisEnrolmentProgressIndicatorComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GisEnrolmentProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
