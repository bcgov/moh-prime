import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusyOverlayComponent } from '../busy-overlay/busy-overlay.component';

describe('BusyOverlayComponent', () => {
  let component: BusyOverlayComponent;
  let fixture: ComponentFixture<BusyOverlayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BusyOverlayComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusyOverlayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
