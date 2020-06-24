import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAdjudicationComponent } from './site-adjudication.component';

describe('SiteAdjudicationComponent', () => {
  let component: SiteAdjudicationComponent;
  let fixture: ComponentFixture<SiteAdjudicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SiteAdjudicationComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAdjudicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
