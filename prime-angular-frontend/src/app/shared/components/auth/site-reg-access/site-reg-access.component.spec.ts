import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegAccessComponent } from './site-reg-access.component';

describe('SiteRegAccessComponent', () => {
  let component: SiteRegAccessComponent;
  let fixture: ComponentFixture<SiteRegAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteRegAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
