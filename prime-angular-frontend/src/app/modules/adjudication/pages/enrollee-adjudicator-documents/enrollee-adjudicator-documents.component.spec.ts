import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeAdjudicatorDocumentsComponent } from './enrollee-adjudicator-documents.component';

describe('EnrolleeAdjudicatorDocumentsComponent', () => {
  let component: EnrolleeAdjudicatorDocumentsComponent;
  let fixture: ComponentFixture<EnrolleeAdjudicatorDocumentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeAdjudicatorDocumentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAdjudicatorDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
