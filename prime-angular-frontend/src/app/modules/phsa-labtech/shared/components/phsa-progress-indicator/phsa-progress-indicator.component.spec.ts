import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhsaProgressIndicatorComponent } from './phsa-progress-indicator.component';

describe('PhsaProgressIndicatorComponent', () => {
  let component: PhsaProgressIndicatorComponent;
  let fixture: ComponentFixture<PhsaProgressIndicatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhsaProgressIndicatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
