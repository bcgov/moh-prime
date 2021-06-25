import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CareSettingPageComponent } from './care-setting-page.component';

describe('CareSettingPageComponent', () => {
  let component: CareSettingPageComponent;
  let fixture: ComponentFixture<CareSettingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CareSettingPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CareSettingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
