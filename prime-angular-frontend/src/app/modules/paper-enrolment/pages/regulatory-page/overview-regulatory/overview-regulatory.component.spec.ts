import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverviewRegulatoryComponent } from './overview-regulatory.component';

describe('OverviewRegulatoryComponent', () => {
  let component: OverviewRegulatoryComponent;
  let fixture: ComponentFixture<OverviewRegulatoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverviewRegulatoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OverviewRegulatoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
