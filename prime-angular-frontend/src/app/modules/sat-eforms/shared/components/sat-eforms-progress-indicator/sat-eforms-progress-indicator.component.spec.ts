import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SatEformsProgressIndicatorComponent } from './sat-eforms-progress-indicator.component';

describe('SatEformsProgressIndicatorComponent', () => {
  let component: SatEformsProgressIndicatorComponent;
  let fixture: ComponentFixture<SatEformsProgressIndicatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SatEformsProgressIndicatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SatEformsProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
