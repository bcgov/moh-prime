import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgBusyModule, InstanceConfigHolderService } from 'ng-busy';

import { BusyOverlayMessageComponent } from './busy-overlay-message.component';
import { busyConfig } from '../busy.config';

describe('BusyOverlayMessageComponent', () => {
  let component: BusyOverlayMessageComponent;
  let fixture: ComponentFixture<BusyOverlayMessageComponent>;

  beforeEach(async(() => {
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
        ]
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
