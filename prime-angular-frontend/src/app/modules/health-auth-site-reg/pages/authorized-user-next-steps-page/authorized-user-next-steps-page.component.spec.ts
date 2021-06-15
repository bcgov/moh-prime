import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedUserNextStepsPageComponent } from './authorized-user-next-steps-page.component';

describe('AuthorizedUserNextStepsPageComponent', () => {
  let component: AuthorizedUserNextStepsPageComponent;
  let fixture: ComponentFixture<AuthorizedUserNextStepsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorizedUserNextStepsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserNextStepsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
