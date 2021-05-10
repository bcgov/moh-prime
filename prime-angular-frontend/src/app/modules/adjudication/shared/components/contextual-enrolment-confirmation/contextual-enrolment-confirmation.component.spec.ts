import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContextualEnrolmentConfirmationComponent } from './contextual-enrolment-confirmation.component';

describe('ContextualEnrolmentConfirmationComponent', () => {
  let component: ContextualEnrolmentConfirmationComponent;
  let fixture: ComponentFixture<ContextualEnrolmentConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContextualEnrolmentConfirmationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContextualEnrolmentConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
