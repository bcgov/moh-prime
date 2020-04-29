import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SameAsComponent } from './same-as.component';

describe('SameAsComponent', () => {
  let component: SameAsComponent;
  let fixture: ComponentFixture<SameAsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SameAsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SameAsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
