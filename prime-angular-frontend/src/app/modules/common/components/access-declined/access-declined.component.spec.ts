import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessDeclinedComponent } from './access-declined.component';
import { CommonModule } from '@angular/common';

describe('AccessDeclinedComponent', () => {
  let component: AccessDeclinedComponent;
  let fixture: ComponentFixture<AccessDeclinedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [CommonModule],
      declarations: []
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessDeclinedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
