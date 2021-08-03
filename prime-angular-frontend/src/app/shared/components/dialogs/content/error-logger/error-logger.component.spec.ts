import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorLoggerComponent } from './error-logger.component';

describe('ErrorLoggerComponent', () => {
  let component: ErrorLoggerComponent;
  let fixture: ComponentFixture<ErrorLoggerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ErrorLoggerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrorLoggerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
