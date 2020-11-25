import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhsaComponent } from './phsa.component';

describe('PhsaComponent', () => {
  let component: PhsaComponent;
  let fixture: ComponentFixture<PhsaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhsaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
