import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoingBusinessAsFormFieldComponent } from './doing-business-as-form-field.component';

describe('DoingBusinessAsFormFieldComponent', () => {
  let component: DoingBusinessAsFormFieldComponent;
  let fixture: ComponentFixture<DoingBusinessAsFormFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoingBusinessAsFormFieldComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoingBusinessAsFormFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
