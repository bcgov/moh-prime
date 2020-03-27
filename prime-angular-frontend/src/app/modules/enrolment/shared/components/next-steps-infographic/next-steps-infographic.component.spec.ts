import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NextStepsInfographicComponent } from './next-steps-infographic.component';
import { SharedModule } from '@shared/shared.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('NextStepsInfographicComponent', () => {
  let component: NextStepsInfographicComponent;
  let fixture: ComponentFixture<NextStepsInfographicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SharedModule,
        EnrolmentModule
      ],
      declarations: []
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextStepsInfographicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
