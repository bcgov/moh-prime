import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { HealthAuthCareTypeOverviewComponent } from './health-auth-care-type-overview.component';

describe('HealthAuthCareTypeOverviewComponent', () => {
  let component: HealthAuthCareTypeOverviewComponent;
  let fixture: ComponentFixture<HealthAuthCareTypeOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        HealthAuthCareTypeOverviewComponent,
        DefaultPipe
      ],
      providers: [

      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
