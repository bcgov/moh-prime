import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgBusyModule } from 'ng-busy';

import { BusyLoadingComponent } from './busy-loading.component';
import { busyConfig } from '../busy.config';

describe('BusyLoadingComponent', () => {
  let component: BusyLoadingComponent;
  let fixture: ComponentFixture<BusyLoadingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgBusyModule.forRoot(busyConfig)
        ],
        declarations: [
          BusyLoadingComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusyLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
