import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthoritySiteSummaryComponent } from './health-authority-site-summary.component';


describe('HealthAuthoritySiteSummaryComponent', () => {
  let component: HealthAuthoritySiteSummaryComponent;
  let fixture: ComponentFixture<HealthAuthoritySiteSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        HealthAuthoritySiteSummaryComponent
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthoritySiteSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
