import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnsupportedComponent } from './unsupported.component';

describe('AccessPreventedComponent', () => {
  let component: UnsupportedComponent;
  let fixture: ComponentFixture<UnsupportedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [UnsupportedComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnsupportedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
