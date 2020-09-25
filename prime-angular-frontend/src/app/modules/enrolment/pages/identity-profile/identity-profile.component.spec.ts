import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IdentityProfileComponent } from './identity-profile.component';

describe('IdentityProfileComponent', () => {
  let component: IdentityProfileComponent;
  let fixture: ComponentFixture<IdentityProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [IdentityProfileComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentityProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
