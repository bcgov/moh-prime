import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareSettingComponent } from './care-setting.component';

describe('CareSettingComponent', () => {
  let component: CareSettingComponent;
  let fixture: ComponentFixture<CareSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CareSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
