import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAdjudicatorDocumentsComponent } from './site-adjudicator-documents.component';

describe('SiteAdjudicatorDocumentsComponent', () => {
  let component: SiteAdjudicatorDocumentsComponent;
  let fixture: ComponentFixture<SiteAdjudicatorDocumentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteAdjudicatorDocumentsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAdjudicatorDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
