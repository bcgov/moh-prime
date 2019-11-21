import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentCertificateComponent } from './enrolment-certificate.component';

describe('EnrolmentCertificateComponent', () => {
  let component: EnrolmentCertificateComponent;
  let fixture: ComponentFixture<EnrolmentCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolmentCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
