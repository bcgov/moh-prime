import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgBusyModule } from 'ng-busy';

import { BusyLoadingComponent } from './busy-loading.component';
import { busyConfig } from '../busy.config';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('BusyLoadingComponent', () => {
  let component: BusyLoadingComponent;
  let fixture: ComponentFixture<BusyLoadingComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgBusyModule.forRoot(busyConfig)
        ],
        declarations: [
          BusyLoadingComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
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
