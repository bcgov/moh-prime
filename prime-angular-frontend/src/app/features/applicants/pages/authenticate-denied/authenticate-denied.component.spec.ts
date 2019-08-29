import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthenticateDeniedComponent } from './authenticate-denied.component';

describe('AuthenticateDeniedComponent', () => {
  let component: AuthenticateDeniedComponent;
  let fixture: ComponentFixture<AuthenticateDeniedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthenticateDeniedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthenticateDeniedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
