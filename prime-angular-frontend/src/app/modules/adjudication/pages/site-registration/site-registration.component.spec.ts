import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationComponent } from './site-registration.component';

describe('SiteRegistrationComponent', () => {
  let component: SiteRegistrationComponent;
  let fixture: ComponentFixture<SiteRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SiteRegistrationComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
