import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IssuancePageComponent } from './issuance-page.component';

describe('IssuancePageComponent', () => {
  let component: IssuancePageComponent;
  let fixture: ComponentFixture<IssuancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IssuancePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IssuancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
