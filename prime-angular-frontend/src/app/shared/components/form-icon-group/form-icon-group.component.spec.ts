import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormIconGroupComponent } from './form-icon-group.component';

describe('FormIconGroupComponent', () => {
  let component: FormIconGroupComponent;
  let fixture: ComponentFixture<FormIconGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormIconGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormIconGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
