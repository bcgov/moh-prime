import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceProviderComponent } from './device-provider.component';

describe('DeviceProviderComponent', () => {
  let component: DeviceProviderComponent;
  let fixture: ComponentFixture<DeviceProviderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DeviceProviderComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceProviderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
