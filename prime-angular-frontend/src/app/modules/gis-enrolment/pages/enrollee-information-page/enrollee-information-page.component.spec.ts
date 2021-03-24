import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeInformationPageComponent } from './enrollee-information-page.component';

describe('EnrolleeInformationPageComponent', () => {
  let component: EnrolleeInformationPageComponent;
  let fixture: ComponentFixture<EnrolleeInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeInformationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
