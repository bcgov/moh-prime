import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthenticateCompleteComponent } from './authenticate-complete.component';

describe('AuthenticateCompleteComponent', () => {
  let component: AuthenticateCompleteComponent;
  let fixture: ComponentFixture<AuthenticateCompleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthenticateCompleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthenticateCompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
