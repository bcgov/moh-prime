import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteNotesPageComponent } from './site-notes-page.component';

describe('SiteNotesPageComponent', () => {
  let component: SiteNotesPageComponent;
  let fixture: ComponentFixture<SiteNotesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteNotesPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteNotesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
