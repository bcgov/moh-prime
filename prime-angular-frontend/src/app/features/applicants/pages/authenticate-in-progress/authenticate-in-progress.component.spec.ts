import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthenticateInProgressComponent } from './authenticate-in-progress.component';

describe('AuthenticateInProgressComponent', () => {
  let component: AuthenticateInProgressComponent;
  let fixture: ComponentFixture<AuthenticateInProgressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthenticateInProgressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthenticateInProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
