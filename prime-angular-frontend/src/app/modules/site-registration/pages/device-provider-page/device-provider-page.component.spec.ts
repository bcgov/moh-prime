import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceProviderPageComponent } from './device-provider-page.component';

describe('DeviceProviderPageComponent', () => {
  let component: DeviceProviderPageComponent;
  let fixture: ComponentFixture<DeviceProviderPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeviceProviderPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceProviderPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
