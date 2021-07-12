import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OboSitesOverviewComponent } from './obo-sites-overview.component';

describe('OboSitesOverviewComponent', () => {
  let component: OboSitesOverviewComponent;
  let fixture: ComponentFixture<OboSitesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OboSitesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OboSitesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
