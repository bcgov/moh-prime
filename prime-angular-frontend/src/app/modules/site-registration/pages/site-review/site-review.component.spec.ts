import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteReviewComponent } from './site-review.component';

describe('SiteReviewComponent', () => {
  let component: SiteReviewComponent;
  let fixture: ComponentFixture<SiteReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
