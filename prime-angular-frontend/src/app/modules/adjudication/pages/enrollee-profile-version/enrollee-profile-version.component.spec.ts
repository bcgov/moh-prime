import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileVersionComponent } from './enrollee-profile-version.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { EnrolleeReviewComponent } from '@shared/components/enrollee/enrollee-review/enrollee-review.component';

describe('EnrolleeProfileVersionComponent', () => {
  let component: EnrolleeProfileVersionComponent;
  let fixture: ComponentFixture<EnrolleeProfileVersionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeProfileVersionComponent,
          PageComponent,
          PageHeaderComponent,
          EnrolleeReviewComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeProfileVersionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
