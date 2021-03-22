import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivacyOfficerPageComponent } from './privacy-officer-page.component';

describe('PrivacyOfficerPageComponent', () => {
  let component: PrivacyOfficerPageComponent;
  let fixture: ComponentFixture<PrivacyOfficerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrivacyOfficerPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivacyOfficerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
