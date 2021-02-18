import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GisLoginPageComponent } from './gis-login-page.component';

describe('GisLoginPageComponent', () => {
  let component: GisLoginPageComponent;
  let fixture: ComponentFixture<GisLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GisLoginPageComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GisLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
