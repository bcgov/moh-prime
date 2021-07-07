import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverviewOboSitesComponent } from './overview-obo-sites.component';

describe('OverviewOboSitesComponent', () => {
  let component: OverviewOboSitesComponent;
  let fixture: ComponentFixture<OverviewOboSitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverviewOboSitesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OverviewOboSitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
