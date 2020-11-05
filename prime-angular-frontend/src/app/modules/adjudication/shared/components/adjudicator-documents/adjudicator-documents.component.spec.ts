import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicatorDocumentsComponent } from './adjudicator-documents.component';

describe('AdjudicatorDocumentsComponent', () => {
  let component: AdjudicatorDocumentsComponent;
  let fixture: ComponentFixture<AdjudicatorDocumentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdjudicatorDocumentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
