import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteEventsPageComponent } from './site-events-page.component';

describe('SiteEventsPageComponent', () => {
  let component: SiteEventsPageComponent;
  let fixture: ComponentFixture<SiteEventsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteEventsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteEventsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
