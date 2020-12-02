import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolmentProgressIndicatorComponent } from './enrolment-progress-indicator.component';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('EnrolmentProgressIndicatorComponent', () => {
  let component: EnrolmentProgressIndicatorComponent;
  let fixture: ComponentFixture<EnrolmentProgressIndicatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        EnrolmentModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
