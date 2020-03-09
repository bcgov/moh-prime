import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SigningAuthorityComponent } from './signing-authority.component';

describe('SigningAuthorityComponent', () => {
  let component: SigningAuthorityComponent;
  let fixture: ComponentFixture<SigningAuthorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SigningAuthorityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SigningAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
