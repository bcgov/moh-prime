import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeOrganizationsComponent } from './enrollee-organizations.component';

describe('EnrolleeOrganizationsComponent', () => {
  let component: EnrolleeOrganizationsComponent;
  let fixture: ComponentFixture<EnrolleeOrganizationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeOrganizationsComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeOrganizationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
