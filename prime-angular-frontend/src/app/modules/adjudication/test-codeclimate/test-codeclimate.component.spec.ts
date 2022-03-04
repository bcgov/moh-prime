import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestCodeclimateComponent } from './test-codeclimate.component';

describe('TestCodeclimateComponent', () => {
  let component: TestCodeclimateComponent;
  let fixture: ComponentFixture<TestCodeclimateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestCodeclimateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestCodeclimateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
