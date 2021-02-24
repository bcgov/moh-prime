import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LdapUserPageComponent } from './ldap-user-page.component';

describe('LdapUserPageComponent', () => {
  let component: LdapUserPageComponent;
  let fixture: ComponentFixture<LdapUserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LdapUserPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LdapUserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
