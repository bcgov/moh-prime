import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessPreventedComponent } from './access-prevented.component';

describe('AccessPreventedComponent', () => {
  let component: AccessPreventedComponent;
  let fixture: ComponentFixture<AccessPreventedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessPreventedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessPreventedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
