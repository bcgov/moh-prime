import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { SubmissionConfirmationPageComponent } from './submission-confirmation-page.component';

xdescribe('SubmissionConfirmationPageComponent', () => {
  let component: SubmissionConfirmationPageComponent;
  let fixture: ComponentFixture<SubmissionConfirmationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [SubmissionConfirmationPageComponent],
      schemas: [NO_ERRORS_SCHEMA]
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
