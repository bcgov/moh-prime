import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileComponent } from './enrollee-profile.component';

describe('EnrolleeProfileComponent', () => {
  let component: EnrolleeProfileComponent;
  let fixture: ComponentFixture<EnrolleeProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
