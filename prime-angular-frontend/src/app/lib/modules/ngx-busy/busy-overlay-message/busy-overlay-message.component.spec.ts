import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgBusyModule, InstanceConfigHolderService } from 'ng-busy';

import { BusyOverlayMessageComponent } from './busy-overlay-message.component';
import { busyConfig } from '../busy.config';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('BusyOverlayMessageComponent', () => {
  let component: BusyOverlayMessageComponent;
  let fixture: ComponentFixture<BusyOverlayMessageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgBusyModule.forRoot(busyConfig)
        ],
        declarations: [
          BusyOverlayMessageComponent
        ],
        providers: [
          {
            provide: 'instanceConfigHolder',
            useClass: InstanceConfigHolderService
          }
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
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
