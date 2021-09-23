import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionConfirmationPageComponent } from './submission-confirmation-page.component';

describe('SubmissionConfirmationPageComponent', () => {
  let component: SubmissionConfirmationPageComponent;
  let fixture: ComponentFixture<SubmissionConfirmationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmissionConfirmationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmissionConfirmationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
