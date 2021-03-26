import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BcscEnrolmentLoginPageComponent } from './bcsc-enrolment-login-page.component';

describe('BcscEnrolmentLoginPageComponent', () => {
  let component: BcscEnrolmentLoginPageComponent;
  let fixture: ComponentFixture<BcscEnrolmentLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BcscEnrolmentLoginPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BcscEnrolmentLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
