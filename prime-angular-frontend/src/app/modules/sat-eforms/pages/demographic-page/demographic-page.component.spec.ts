import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemographicPageComponent } from './demographic-page.component';

describe('DemographicPageComponent', () => {
  let component: DemographicPageComponent;
  let fixture: ComponentFixture<DemographicPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DemographicPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DemographicPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
