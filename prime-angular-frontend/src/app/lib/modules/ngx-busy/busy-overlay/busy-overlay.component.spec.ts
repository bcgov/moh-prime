import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgBusyModule } from 'ng-busy';

import { BusyOverlayComponent } from '../busy-overlay/busy-overlay.component';
import { busyConfig } from '../busy.config';

describe('BusyOverlayComponent', () => {
  let component: BusyOverlayComponent;
  let fixture: ComponentFixture<BusyOverlayComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgBusyModule.forRoot(busyConfig)
        ],
        declarations: [
          BusyOverlayComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
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
