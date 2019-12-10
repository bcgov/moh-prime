import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicatorNotesComponent } from './adjudicator-notes.component';

describe('AdjudicatorNotesComponent', () => {
  let component: AdjudicatorNotesComponent;
  let fixture: ComponentFixture<AdjudicatorNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdjudicatorNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
