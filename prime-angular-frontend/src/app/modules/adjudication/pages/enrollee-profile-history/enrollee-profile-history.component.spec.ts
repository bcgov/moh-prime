import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileHistoryComponent } from './enrollee-profile-history.component';
import { PageComponent } from '@shared/components/page/page.component';

describe('EnrolleeProfileHistoryComponent', () => {
  let component: EnrolleeProfileHistoryComponent;
  let fixture: ComponentFixture<EnrolleeProfileHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeProfileHistoryComponent,
          PageComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeProfileHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
