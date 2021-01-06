import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailableAccessComponent } from './available-access.component';

describe('AvailableAccessComponent', () => {
  let component: AvailableAccessComponent;
  let fixture: ComponentFixture<AvailableAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvailableAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AvailableAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
