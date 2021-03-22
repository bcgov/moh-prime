import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HoursOperationPageComponent } from './hours-operation-page.component';

describe('HoursOperationPageComponent', () => {
  let component: HoursOperationPageComponent;
  let fixture: ComponentFixture<HoursOperationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HoursOperationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursOperationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
