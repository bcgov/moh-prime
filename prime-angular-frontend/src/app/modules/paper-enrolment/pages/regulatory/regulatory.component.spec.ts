import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegulatoryComponent } from './regulatory.component';

describe('RegulatoryComponent', () => {
  let component: RegulatoryComponent;
  let fixture: ComponentFixture<RegulatoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegulatoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
