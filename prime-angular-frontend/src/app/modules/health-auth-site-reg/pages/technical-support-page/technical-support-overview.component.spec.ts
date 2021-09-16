import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnicalSupportOverviewComponent } from './technical-support-overview.component';

describe('TechnicalSupportOverviewComponent', () => {
  let component: TechnicalSupportOverviewComponent;
  let fixture: ComponentFixture<TechnicalSupportOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TechnicalSupportOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
