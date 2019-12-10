import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentCertificateNotesComponent } from './enrolment-certificate-notes.component';

describe('EnrolmentCertificateNotesComponent', () => {
  let component: EnrolmentCertificateNotesComponent;
  let fixture: ComponentFixture<EnrolmentCertificateNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolmentCertificateNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentCertificateNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
