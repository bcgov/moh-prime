import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationsComponent } from './site-registrations.component';

describe('SiteRegistrationsComponent', () => {
  let component: SiteRegistrationsComponent;
  let fixture: ComponentFixture<SiteRegistrationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegistrationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
