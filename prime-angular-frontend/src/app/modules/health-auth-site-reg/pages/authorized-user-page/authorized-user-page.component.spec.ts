import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedUserPageComponent } from './authorized-user-page.component';

describe('AuthorizedUserPageComponent', () => {
  let component: AuthorizedUserPageComponent;
  let fixture: ComponentFixture<AuthorizedUserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorizedUserPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
