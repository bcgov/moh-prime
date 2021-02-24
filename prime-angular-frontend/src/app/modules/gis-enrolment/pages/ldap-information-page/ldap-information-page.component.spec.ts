import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LdapInformationPageComponent } from './ldap-information-page.component';

describe('LdapInformationPageComponent', () => {
  let component: LdapInformationPageComponent;
  let fixture: ComponentFixture<LdapInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LdapInformationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LdapInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
