import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoteAccessAddressesComponent } from './remote-access-addresses.component';

describe('RemoteAccessAddressesComponent', () => {
  let component: RemoteAccessAddressesComponent;
  let fixture: ComponentFixture<RemoteAccessAddressesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoteAccessAddressesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteAccessAddressesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
