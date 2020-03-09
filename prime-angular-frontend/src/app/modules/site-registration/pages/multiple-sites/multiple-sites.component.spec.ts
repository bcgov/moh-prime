import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleSitesComponent } from './multiple-sites.component';

describe('MultipleSitesComponent', () => {
  let component: MultipleSitesComponent;
  let fixture: ComponentFixture<MultipleSitesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MultipleSitesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MultipleSitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
