import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IdentitySubmissionComponent } from './identity-submission.component';

describe('IdentitySubmissionComponent', () => {
  let component: IdentitySubmissionComponent;
  let fixture: ComponentFixture<IdentitySubmissionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IdentitySubmissionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentitySubmissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
