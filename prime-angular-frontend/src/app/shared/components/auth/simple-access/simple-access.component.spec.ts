import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimpleAccessComponent } from './simple-access.component';

describe('SimpleAccessComponent', () => {
  let component: SimpleAccessComponent;
  let fixture: ComponentFixture<SimpleAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimpleAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimpleAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
