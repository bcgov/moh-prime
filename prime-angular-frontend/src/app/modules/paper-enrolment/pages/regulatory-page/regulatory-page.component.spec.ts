import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegulatoryPageComponent } from './regulatory-page.component';

describe('RegulatoryPageComponent', () => {
  let component: RegulatoryPageComponent;
  let fixture: ComponentFixture<RegulatoryPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegulatoryPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
