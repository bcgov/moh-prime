import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileVersionComponent } from './enrollee-profile-version.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

describe('EnrolleeProfileVersionComponent', () => {
  let component: EnrolleeProfileVersionComponent;
  let fixture: ComponentFixture<EnrolleeProfileVersionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeProfileVersionComponent,
          PageComponent,
          PageHeaderComponent
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
