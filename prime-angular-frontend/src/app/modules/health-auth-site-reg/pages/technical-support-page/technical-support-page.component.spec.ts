import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechnicalSupportPageComponent } from './technical-support-page.component';

describe('TechnicalSupportPageComponent', () => {
  let component: TechnicalSupportPageComponent;
  let fixture: ComponentFixture<TechnicalSupportPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TechnicalSupportPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
