import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedUserApprovedPageComponent } from './authorized-user-approved-page.component';

describe('AuthorizedUserApprovedPageComponent', () => {
  let component: AuthorizedUserApprovedPageComponent;
  let fixture: ComponentFixture<AuthorizedUserApprovedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorizedUserApprovedPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserApprovedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
