import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OboAccessTermsComponent } from './obo-access-terms.component';

describe('OboAccessTermsComponent', () => {
  let component: OboAccessTermsComponent;
  let fixture: ComponentFixture<OboAccessTermsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [OboAccessTermsComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OboAccessTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
