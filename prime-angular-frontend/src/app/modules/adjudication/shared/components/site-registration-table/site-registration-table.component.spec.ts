import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationTableComponent } from './site-registration-table.component';

describe('SiteRegistrationTableComponent', () => {
  let component: SiteRegistrationTableComponent;
  let fixture: ComponentFixture<SiteRegistrationTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegistrationTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
