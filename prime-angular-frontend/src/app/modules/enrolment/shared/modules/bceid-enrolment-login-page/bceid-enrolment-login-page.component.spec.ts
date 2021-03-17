import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BceidEnrolmentLoginPageComponent } from './bceid-enrolment-login-page.component';

describe('BceidEnrolmentLoginPageComponent', () => {
  let component: BceidEnrolmentLoginPageComponent;
  let fixture: ComponentFixture<BceidEnrolmentLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BceidEnrolmentLoginPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BceidEnrolmentLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
