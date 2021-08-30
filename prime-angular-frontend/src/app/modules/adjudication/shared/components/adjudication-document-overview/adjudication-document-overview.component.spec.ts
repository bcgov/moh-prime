import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicationDocumentOverviewComponent } from './adjudication-document-overview.component';

describe('AdjudicationDocumentOverviewComponent', () => {
  let component: AdjudicationDocumentOverviewComponent;
  let fixture: ComponentFixture<AdjudicationDocumentOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdjudicationDocumentOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicationDocumentOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
