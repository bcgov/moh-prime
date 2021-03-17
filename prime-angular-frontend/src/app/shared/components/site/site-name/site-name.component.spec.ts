import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteNameComponent } from './site-name.component';

describe('SiteNameComponent', () => {
  let component: SiteNameComponent;
  let fixture: ComponentFixture<SiteNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteNameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
