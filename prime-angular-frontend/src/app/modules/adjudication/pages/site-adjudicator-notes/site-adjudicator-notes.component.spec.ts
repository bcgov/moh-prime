import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAdjudicatorNotesComponent } from './site-adjudicator-notes.component';

describe('SiteAdjudicatorNotesComponent', () => {
  let component: SiteAdjudicatorNotesComponent;
  let fixture: ComponentFixture<SiteAdjudicatorNotesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteAdjudicatorNotesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAdjudicatorNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
