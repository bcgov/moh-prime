import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationClaimedPageComponent } from './organization-claimed-page.component';

describe('OrganizationClaimedPageComponent', () => {
  let component: OrganizationClaimedPageComponent;
  let fixture: ComponentFixture<OrganizationClaimedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationClaimedPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationClaimedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
