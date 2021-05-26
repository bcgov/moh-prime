import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedUserDeclinedPageComponent } from './authorized-user-declined-page.component';

describe('AuthorizedUserDeclinedPageComponent', () => {
  let component: AuthorizedUserDeclinedPageComponent;
  let fixture: ComponentFixture<AuthorizedUserDeclinedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorizedUserDeclinedPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserDeclinedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
