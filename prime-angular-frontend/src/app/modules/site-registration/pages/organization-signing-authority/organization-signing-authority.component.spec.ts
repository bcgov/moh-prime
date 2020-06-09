import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationSigningAuthorityComponent } from './organization-signing-authority.component';

describe('OrganizationSigningAuthorityComponent', () => {
  let component: OrganizationSigningAuthorityComponent;
  let fixture: ComponentFixture<OrganizationSigningAuthorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrganizationSigningAuthorityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationSigningAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
