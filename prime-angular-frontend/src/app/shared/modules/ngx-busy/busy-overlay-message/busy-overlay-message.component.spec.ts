import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusyOverlayMessageComponent } from './busy-overlay-message.component';

describe('BusyOverlayMessageComponent', () => {
  let component: BusyOverlayMessageComponent;
  let fixture: ComponentFixture<BusyOverlayMessageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BusyOverlayMessageComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusyOverlayMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
