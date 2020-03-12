import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorComponent } from './vendor.component';
import { SharedModule } from '@shared/shared.module';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('VendorComponent', () => {
  let component: VendorComponent;
  let fixture: ComponentFixture<VendorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
