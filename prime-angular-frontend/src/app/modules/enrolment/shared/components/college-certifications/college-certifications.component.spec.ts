import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CollegeCertificationsComponent } from './college-certifications.component';

describe('CollegeCertificationsComponent', () => {
  let component: CollegeCertificationsComponent;
  let fixture: ComponentFixture<CollegeCertificationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CollegeCertificationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollegeCertificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
