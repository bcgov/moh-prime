import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverviewCareSettingComponent } from './overview-care-setting.component';

describe('OverviewCareSettingComponent', () => {
  let component: OverviewCareSettingComponent;
  let fixture: ComponentFixture<OverviewCareSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OverviewCareSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OverviewCareSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
