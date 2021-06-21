import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnicalSupportsPageComponent } from './technical-supports-page.component';

describe('TechnicalSupportsPageComponent', () => {
  let component: TechnicalSupportsPageComponent;
  let fixture: ComponentFixture<TechnicalSupportsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TechnicalSupportsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
