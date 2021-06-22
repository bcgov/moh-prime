import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OboSitesComponent } from './obo-sites.component';

describe('OboSitesComponent', () => {
  let component: OboSitesComponent;
  let fixture: ComponentFixture<OboSitesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OboSitesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OboSitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
