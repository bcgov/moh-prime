import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorsPageComponent } from './administrators-page.component';

describe('AdministratorsPageComponent', () => {
  let component: AdministratorsPageComponent;
  let fixture: ComponentFixture<AdministratorsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministratorsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
