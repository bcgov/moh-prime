import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileHistoriesComponent } from './enrollee-profile-histories.component';
import { PageComponent } from '@shared/components/page/page.component';

describe('EnrolleeProfileHistoriesComponent', () => {
  let component: EnrolleeProfileHistoriesComponent;
  let fixture: ComponentFixture<EnrolleeProfileHistoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeProfileHistoriesComponent,
          PageComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeProfileHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
