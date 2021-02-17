import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToggleContentComponent } from './toggle-content.component';

describe('ToggleContentComponent', () => {
  let component: ToggleContentComponent;
  let fixture: ComponentFixture<ToggleContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToggleContentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToggleContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
