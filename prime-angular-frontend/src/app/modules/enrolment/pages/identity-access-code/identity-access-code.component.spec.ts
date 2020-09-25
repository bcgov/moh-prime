import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IdentityAccessCodeComponent } from './identity-access-code.component';

describe('IdentityAccessCodeComponent', () => {
  let component: IdentityAccessCodeComponent;
  let fixture: ComponentFixture<IdentityAccessCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [IdentityAccessCodeComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentityAccessCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
