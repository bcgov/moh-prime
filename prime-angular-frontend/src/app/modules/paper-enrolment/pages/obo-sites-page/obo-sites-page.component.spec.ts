import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OboSitesPageComponent } from './obo-sites-page.component';

describe('OboSitesPageComponent', () => {
  let component: OboSitesPageComponent;
  let fixture: ComponentFixture<OboSitesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OboSitesPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OboSitesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
