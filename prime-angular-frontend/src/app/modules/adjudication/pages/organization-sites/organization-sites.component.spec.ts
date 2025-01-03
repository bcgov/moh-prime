import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationSitesComponent } from './organization-sites.component';

describe('OrganizationSitesComponent', () => {
  let component: OrganizationSitesComponent;
  let fixture: ComponentFixture<OrganizationSitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationSitesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationSitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
