import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicationContainerComponent } from './adjudication-container.component';

describe('AdjudicationContainerComponent', () => {
  let component: AdjudicationContainerComponent;
  let fixture: ComponentFixture<AdjudicationContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdjudicationContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicationContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
