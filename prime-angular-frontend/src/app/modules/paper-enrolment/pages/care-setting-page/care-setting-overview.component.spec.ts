import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareSettingOverviewComponent } from './care-setting-overview.component';

describe('CareSettingOverviewComponent', () => {
  let component: CareSettingOverviewComponent;
  let fixture: ComponentFixture<CareSettingOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareSettingOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareSettingOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
