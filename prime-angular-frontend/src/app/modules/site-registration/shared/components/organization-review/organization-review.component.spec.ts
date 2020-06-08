import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationReviewComponent } from './organization-review.component';

describe('OrganizationReviewComponent', () => {
  let component: OrganizationReviewComponent;
  let fixture: ComponentFixture<OrganizationReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrganizationReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
