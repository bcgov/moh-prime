import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegulatoryInfoComponent } from './regulatory.component';

describe('RegulatoryInfoComponent', () => {
  let component: RegulatoryInfoComponent;
  let fixture: ComponentFixture<RegulatoryInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RegulatoryInfoComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
