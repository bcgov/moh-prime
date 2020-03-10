import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivacyOfficerComponent } from './privacy-officer.component';

describe('PrivacyOfficerComponent', () => {
  let component: PrivacyOfficerComponent;
  let fixture: ComponentFixture<PrivacyOfficerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrivacyOfficerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivacyOfficerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
