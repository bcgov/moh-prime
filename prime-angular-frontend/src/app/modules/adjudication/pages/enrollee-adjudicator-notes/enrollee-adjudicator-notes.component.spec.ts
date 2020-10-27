import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeAdjudicatorNotesComponent } from './enrollee-adjudicator-notes.component';

describe('EnrolleeAdjudicatorNotesComponent', () => {
  let component: EnrolleeAdjudicatorNotesComponent;
  let fixture: ComponentFixture<EnrolleeAdjudicatorNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeAdjudicatorNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAdjudicatorNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
