import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileVersionsComponent } from './enrollee-profile-versions.component';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

describe('EnrolleeProfileVersionsComponent', () => {
  let component: EnrolleeProfileVersionsComponent;
  let fixture: ComponentFixture<EnrolleeProfileVersionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeProfileVersionsComponent,
          PageComponent,
          PageHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeProfileVersionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
