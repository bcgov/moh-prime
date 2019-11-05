import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceProviderInfoComponent } from './device-provider.component';

describe('DeviceProviderInfoComponent', () => {
  let component: DeviceProviderInfoComponent;
  let fixture: ComponentFixture<DeviceProviderInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DeviceProviderInfoComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceProviderInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
