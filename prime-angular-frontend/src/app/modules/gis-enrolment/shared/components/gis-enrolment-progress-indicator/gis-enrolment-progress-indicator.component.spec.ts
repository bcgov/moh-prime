import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GisEnrolmentProgressIndicatorComponent } from './gis-enrolment-progress-indicator.component';

describe('GisEnrolmentProgressIndicatorComponent', () => {
  let component: GisEnrolmentProgressIndicatorComponent;
  let fixture: ComponentFixture<GisEnrolmentProgressIndicatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GisEnrolmentProgressIndicatorComponent]
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
